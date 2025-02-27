namespace BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Models.Validators.Vehicles;

using FluentValidation;

internal class AuctionValidator : BaseValidator<Auction>
{
    public AuctionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.CreatedDate)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.VehicleId)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.Vehicle)
            .SetValidator(new VehicleValidator())
            .When(x => x.Vehicle is not null);
    }
}