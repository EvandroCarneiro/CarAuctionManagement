namespace BCA.CarAuctionManagement.Infrastructure.IoC;

using System.Reflection;

using BCA.CarAuctionManagement.Infrastructure.Dependencies.Mediator.Pipelines;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

internal static class MediatorDependencyInjection
{
    public static IServiceCollection ConfigureMediator(this IServiceCollection services, Assembly domainAssembly)
    {
        return services
            .AddPipelineBehaviours()
            .AddMediatR(config => config.RegisterServicesFromAssembly(domainAssembly));
    }

    private static IServiceCollection AddPipelineBehaviours(this IServiceCollection services)
        => services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatePipelineBehaviour<,>));
}
