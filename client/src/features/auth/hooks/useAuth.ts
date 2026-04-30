import { useMutation } from "@tanstack/react-query";
import { useDispatch } from "react-redux";
import { authService } from "../services/authService";
import { setUser, logoutAction } from "../store/authSlice";
import { useNavigate } from "react-router-dom";
import type { LoginRequest, RegisterUserRequest } from "../types/authTypes";

export const useAuth = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  // Login Mutasyonu
  const loginMutation = useMutation({
    mutationFn: (credentials: LoginRequest) => authService.login(credentials),
    onSuccess: (response) => {
      if (response.success && response.data) {
        // 1. Token'ı kaydet
        localStorage.setItem("accessToken", response.data.accessToken);

        // 2. Redux State'ini güncelle
        dispatch(setUser(response.data.user));

        // 3. Yönlendir
        navigate("/dashboard");
      }
    },
  });

  // Register Mutasyonu
  const registerMutation = useMutation({
    mutationFn: (data: RegisterUserRequest) => authService.register(data),
    onSuccess: (response) => {
      if (response.success) {
        // Kayıt başarılıysa login'e yönlendir veya otomatik login yap
        navigate("/login");
      }
    },
  });

  const logout = () => {
    dispatch(logoutAction());
    navigate("/login");
  };

  return {
    login: loginMutation.mutate,
    isLoginLoading: loginMutation.isPending,
    register: registerMutation.mutate,
    isRegisterLoading: registerMutation.isPending,
    logout,
  };
};