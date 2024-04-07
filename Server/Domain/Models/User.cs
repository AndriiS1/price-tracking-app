using Domain.Enums;

namespace Domain.Models;

public class User
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? RefreshToken { get; set; }
    public required UserRole Role { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}