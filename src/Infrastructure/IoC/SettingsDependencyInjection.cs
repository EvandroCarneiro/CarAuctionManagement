namespace BCA.CarAuctionManagement.Infrastructure.IoC;

using BCA.CarAuctionManagement.Infrastructure.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class SettingsDependencyInjection
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
        => services
            .AddDependencySettings();

    private static IServiceCollection AddDependencySettings(this IServiceCollection services)
        => services
            .AddSingleton(provider => provider.GetRequiredService<IConfiguration>()
                .GetSection(Constants.Dependencies.SqlServer)
                .Get<SqlServerDependencySetting>());
}
