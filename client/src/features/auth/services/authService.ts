import { apiClient } from "@/core/api/apiClient";
import type { ApiResponse } from "@/core/types/ApiResponse";
import type {
  LoginRequest,
  RegisterUserRequest,
  TokenResponseDto,
  CreatedUserResponseDto
} from "../types/authTypes";

export const authService = {
  login: async (request: LoginRequest): Promise<ApiResponse<TokenResponseDto>> =>
    await apiClient<TokenResponseDto>("/authentication/login", {
      method: "POST",
      body: JSON.stringify(request),
    }),

  register: async (request: RegisterUserRequest): Promise<ApiResponse<CreatedUserResponseDto>> =>
    await apiClient<CreatedUserResponseDto>("/authentication/register", {
      method: "POST",
      body: JSON.stringify(request),
    }),

  refreshToken: async (refreshToken: string): Promise<ApiResponse<TokenResponseDto>> =>
    await apiClient<TokenResponseDto>("/authentication/refresh-token", {
      method: "POST",
      body: JSON.stringify(refreshToken),
    }),

  revokeToken: async (refreshToken: string): Promise<ApiResponse<null>> =>
    await apiClient<null>("/authentication/revoke-token", {
      method: "POST",
      body: JSON.stringify(refreshToken),
    })
};