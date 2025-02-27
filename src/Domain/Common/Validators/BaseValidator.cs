namespace BCA.CarAuctionManagement.Domain.Common.Validators;

using FluentValidation;

internal abstract class BaseValidator<T> : AbstractValidator<T> where T : class
{
}
