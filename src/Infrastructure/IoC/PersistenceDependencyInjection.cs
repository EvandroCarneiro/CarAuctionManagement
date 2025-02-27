namespace BCA.CarAuctionManagement.Infrastructure.IoC;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories;

using Microsoft.Extensions.DependencyInjection;

internal static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
        => services
            .AddSingleton<ISqlDb, SqlDb>()
            .ConfigureRepositories();

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        => services
            .AddScoped<IAuctionRepository, AuctionRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IVehicleRepository, VehicleRepository>();
}
