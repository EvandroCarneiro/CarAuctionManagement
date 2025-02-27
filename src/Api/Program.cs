namespace BCA.CarAuctionManagement.Api;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using BCA.CarAuctionManagement.Api.Extensions;
using BCA.CarAuctionManagement.Api.Middleware;
using BCA.CarAuctionManagement.Infrastructure;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        WebApplication.CreateBuilder(args)
            .ConfigureAndBuild()
            .Run();
    }

    private static WebApplication ConfigureAndBuild(this WebApplicationBuilder builder)
    {
        builder
            .Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("conf/appsettings.json", true, true)
            .AddJsonFile($"conf/appsettings.{builder.Environment.EnvironmentName}.json", true, true);

        builder
            .Services
            .ConfigureSwagger()
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.ConfigureInfrastructure(builder.Configuration);

        var app = builder.Build();

        app.MapControllers();

        app
            .UseMiddleware<ExceptionMiddleware>()
            .AddSwagger();

        return app;
    }
}
