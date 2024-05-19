using Infrastructure;
using ServerPresentation.Extensions;
using UseCase;

namespace ServerPresentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.ConfigureInfrastructure();
        builder.ConfigureUseCase();
        builder.ConfigureJwt();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomizedSwagger();
        builder.Services.AddCustomCors();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}