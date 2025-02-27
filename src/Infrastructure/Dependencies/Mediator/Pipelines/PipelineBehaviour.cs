namespace BCA.CarAuctionManagement.Infrastructure.Dependencies.Mediator.Pipelines;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

internal abstract class PipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public abstract Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);
}
