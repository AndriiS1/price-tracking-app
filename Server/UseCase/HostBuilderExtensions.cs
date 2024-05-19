using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UseCase;

public static class HostBuilderExtensions
{
    public static void ConfigureUseCase(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(HostBuilderExtensions).Assembly));
    }
}