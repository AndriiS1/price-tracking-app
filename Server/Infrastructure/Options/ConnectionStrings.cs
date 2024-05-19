namespace Infrastructure.Options;

public class ConnectionStrings
{
    public static readonly string Section = "ConnectionStrings";
    public string Database { get; set; } = string.Empty;
}