namespace Domain.Dto;

public class RefreshTokenDataDto
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}