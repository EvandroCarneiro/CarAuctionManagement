namespace BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Validators.Vehicles;

using FluentValidation;

internal class CreateVehicleCommandValidator : BaseValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(x => x.Vehicle)
            .NotNull()
            .WithMessage(ValidationMessages.RequiredField)
            .SetValidator(new VehicleValidator());
    }
}
