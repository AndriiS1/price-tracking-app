namespace Infrastructure.Options;

public class JwtOptions
{
    public static readonly string Section = "Jwt";
    public string Key { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string TokenValidityInMinutes { get; set; } = string.Empty;
    public string RefreshTokenValidityInDays { get; set; } = string.Empty;
}