import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { authService } from "../services/authService";
import type { LoginRequest, TokenResponseDto } from "../types/authTypes";
import type { UserResponseDto } from "@/features/users/types/userTypes";

interface AuthState {
  user: UserResponseDto | null;
  isAuthenticated: boolean;
  loading: boolean;
}

const initialState: AuthState = {
  user: null,
  isAuthenticated: !!localStorage.getItem("accessToken"),
  loading: false,
};

export const loginAction = createAsyncThunk(
  "auth/login",
  async (credentials: LoginRequest, { rejectWithValue }) => {
    const response = await authService.login(credentials);

    if (response.success && response.data) {
      localStorage.setItem("accessToken", response.data.accessToken);
      return response.data;
    }

    return rejectWithValue(response.message);
  }
);

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logoutAction: (state) => {
      state.user = null;
      state.isAuthenticated = false;
      localStorage.removeItem("accessToken");
    },
    setUser: (state, action: PayloadAction<UserResponseDto>) => {
      state.user = action.payload;
      state.isAuthenticated = true;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginAction.pending, (state) => {
        state.loading = true;
      })
      .addCase(loginAction.fulfilled, (state, action: PayloadAction<TokenResponseDto>) => {
        state.loading = false;
        state.isAuthenticated = true;
        state.user = action.payload.user;
      })
      .addCase(loginAction.rejected, (state) => {
        state.loading = false;
        state.isAuthenticated = false;
        state.user = null;
      });
  },
});

export const { logoutAction, setUser } = authSlice.actions;
export default authSlice.reducer;