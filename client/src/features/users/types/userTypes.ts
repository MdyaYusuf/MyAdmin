import type { RoleResponseDto } from "@/features/roles/types/roleTypes";

export interface UserResponseDto {
  id: string;
  username: string;
  email: string;
  profileImageUrl: string | null;
  bio: string | null;
  isActive: boolean;
  createdDate: string;
  updatedDate: string | null;
  roles: RoleResponseDto[];
}

export interface RegisterUserRequest {
  username: string;
  email: string;
  password: string;
  profileImageUrl: string | null;
  bio: string | null;
}

export interface UpdateUserRequest {
  username: string;
  email: string;
  bio: string | null;
  imageFile: File | null;
}

export interface CreatedUserResponseDto {
  id: string;
  username: string;
  email: string;
}

export interface UserPreviewDto {
  id: string;
  username: string;
  profileImageUrl: string | null;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
  confirmNewPassword: string;
}