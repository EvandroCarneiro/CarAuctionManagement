namespace BCA.CarAuctionManagement.Domain.Models.Validators.Vehicles;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;

using FluentValidation;

internal class VehicleValidator : BaseValidator<Vehicle>
{
    private const string InvalidFieldForSpecifiedType = "{0} is invalid for specified vehicle type";

    public VehicleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.CreatedDate)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.Type)
            .IsInEnum()
            .NotEqual(Enums.VehicleType.Undefined)
            .WithMessage(ValidationMessages.InvalidField);

        RuleFor(x => x.Manufacturer)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.Model)
            .NotNull()
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.Year)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x.StartingBid)
            .GreaterThan(decimal.Zero)
            .WithMessage(ValidationMessages.RequiredField);

        RuleFor(x => x)
            .Custom(ValidateConditionalProperties);
    }

    private void ValidateConditionalProperties(Vehicle vehicle, ValidationContext<Vehicle> context)
    {
        var errorList = new List<string>();
        var customRequiredMessage = ValidationMessages.RequiredField.Replace("{PropertyName}", "{0}");

        void TryAdd(Func<bool> validate, string message, string fieldName)
        {
            if (validate()) errorList.Add(string.Format(message, fieldName));
        }

        Action validate = vehicle.Type switch
        {
            Enums.VehicleType.Hatchback or
            Enums.VehicleType.Sedan =>
                () =>
                {
                    TryAdd(() => vehicle.NumberOfDoors <= 0, customRequiredMessage, nameof(vehicle.NumberOfDoors));
                    TryAdd(() => vehicle.NumberOfSeats.HasValue, InvalidFieldForSpecifiedType, nameof(vehicle.NumberOfSeats));
                    TryAdd(() => vehicle.LoadCapacity.HasValue, InvalidFieldForSpecifiedType, nameof(vehicle.LoadCapacity));
                }
            ,
            Enums.VehicleType.SUV =>
                () =>
                {
                    TryAdd(() => vehicle.NumberOfSeats <= 0, customRequiredMessage, nameof(vehicle.NumberOfSeats));
                    TryAdd(() => vehicle.NumberOfDoors.HasValue, InvalidFieldForSpecifiedType, nameof(vehicle.NumberOfDoors));
                    TryAdd(() => vehicle.LoadCapacity.HasValue, InvalidFieldForSpecifiedType, nameof(vehicle.LoadCapacity));
                }
            ,
            Enums.VehicleType.Truck =>
                () =>
                {
                    TryAdd(() => vehicle.LoadCapacity <= decimal.Zero, customRequiredMessage, nameof(vehicle.LoadCapacity));
                    TryAdd(() => vehicle.NumberOfDoors.HasValue, InvalidFieldForSpecifiedType, nameof(vehicle.NumberOfDoors));
                    TryAdd(() => vehicle.NumberOfSeats.HasValue, InvalidFieldForSpecifiedType, nameof(vehicle.NumberOfSeats));
                }
            ,
            _ => null,
        };

        validate?.Invoke();

        foreach (var error in errorList)
        {
            context.AddFailure(error);
        }
    }
}
