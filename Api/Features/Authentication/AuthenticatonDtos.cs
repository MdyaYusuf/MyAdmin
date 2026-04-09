using Api.Features.Users;

namespace Api.Features.Authentication;

public record TokenResponseDto(
  string AccessToken,
  DateTime Expiration,
  string RefreshToken,
  UserResponseDto User);
public sealed record LoginRequest(string Email, string Password);
public sealed record RegisterUserRequest(string Username, string Email, string Password);