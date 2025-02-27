namespace BCA.CarAuctionManagement.Api.Extensions;

using Microsoft.OpenApi.Models;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        => services
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Car Auction Management API",
                    Description = "BCA - Car Auction Management System - Code Challenge"
                });
            });

    public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1 Endpoints");
            });
}
