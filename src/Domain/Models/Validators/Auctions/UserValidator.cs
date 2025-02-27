namespace BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Auctions;

using FluentValidation;

internal class UserValidator : BaseValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.CreatedDate)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);
    }
}
