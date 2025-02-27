namespace BCA.CarAuctionManagement.Infrastructure;

using System.Reflection;

using BCA.CarAuctionManagement.Infrastructure.IoC;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class InfrastructureInitializer
{
    public static void ConfigureInfrastructure(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        var domainAssembly = AppDomain.CurrentDomain.Load("BCA.CarAuctionManagement.Domain");

        builder
            .Services
            .AddSettings()
            .ConfigureDatabase()
            .ConfigureMediator(domainAssembly)
            .ConfigureValidators(domainAssembly);
    }

    private static IServiceCollection ConfigureValidators(this IServiceCollection services, Assembly domainAssembly)
        => services
            .AddValidatorsFromAssembly(domainAssembly, ServiceLifetime.Transient, includeInternalTypes: true);
}
