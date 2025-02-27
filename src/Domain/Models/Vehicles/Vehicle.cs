namespace BCA.CarAuctionManagement.Domain.Models.Vehicles;

using System;

using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Enums;

public class Vehicle : Entity<Guid>
{
    public Vehicle(
        VehicleType type,
        string externalId,
        string manufacturer,
        string model,
        int year,
        decimal startingBid,
        int? numberOfDoors,
        int? numberOfSeats,
        decimal? loadCapacity)
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTimeOffset.UtcNow;
        Type = type;
        ExternalId = externalId;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        StartingBid = startingBid;
        NumberOfDoors = numberOfDoors;
        NumberOfSeats = numberOfSeats;
        LoadCapacity = loadCapacity;
    }

    public Vehicle(
        Guid id,
        DateTimeOffset createdDate,
        VehicleType type,
        string externalId,
        string manufacturer,
        string model,
        int year,
        decimal startingBid,
        int? numberOfDoors,
        int? numberOfSeats,
        decimal? loadCapacity)
        : this(
            type,
            externalId,
            manufacturer,
            model,
            year,
            startingBid,
            numberOfDoors,
            numberOfSeats,
            loadCapacity)
    {
        Id = id;
        CreatedDate = createdDate;
    }

    public VehicleType Type { get; private set; }

    public string ExternalId { get; private set; }

    public string Manufacturer { get; private set; }

    public string Model { get; private set; }

    public int Year { get; private set; }

    public decimal StartingBid { get; private set; }

    public int? NumberOfDoors { get; private set; }

    public int? NumberOfSeats { get; private set; }

    public decimal? LoadCapacity { get; private set; }
}
