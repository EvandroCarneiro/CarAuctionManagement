namespace BCA.CarAuctionManagement.Infrastructure.Dependencies.Mediator.Pipelines;

using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using BCA.CarAuctionManagement.Domain.Requests.Base;

internal class ValidatePipelineBehaviour<TRequest, TResponse>(IServiceProvider serviceProvider) : PipelineBehaviour<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : FluentResults.ResultBase, new()
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public override async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (request is Request<TResponse> validationRequest)
        {
            var requestValidator = serviceProvider.GetService<IValidator<TRequest>>()
                ?? throw new NotImplementedException($"Missing request validator for type {request.GetType().Name}");

            if (!validationRequest.IsValid(requestValidator))
            {
                var result = new TResponse();

                foreach (var validationError in validationRequest.ValidationResult.Errors)
                {
                    result.Reasons.Add(new FluentResults.Error(validationError.ErrorMessage));
                }

                return result;
            }
        }

        return await next();
    }
}
