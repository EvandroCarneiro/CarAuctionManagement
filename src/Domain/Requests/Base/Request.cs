namespace BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

public class Request<TResponse> : IRequest<TResponse>
    where TResponse : FluentResults.ResultBase
{
    public ValidationResult ValidationResult { get; private set; }

    public bool IsValid(IValidator validator)
    {
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        var context = new ValidationContext<Request<TResponse>>(this);

        ValidationResult = validator.Validate(context);

        return ValidationResult.IsValid;
    }
}