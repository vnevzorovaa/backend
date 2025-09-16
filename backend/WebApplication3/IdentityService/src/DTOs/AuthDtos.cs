namespace IdentityService.DTOs;

public record RegisterRequest(string Email, string Password, string Role);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token);