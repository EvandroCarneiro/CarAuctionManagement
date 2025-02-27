namespace BCA.CarAuctionManagement.Infrastructure.Persistence.DBOs;

using BCA.CarAuctionManagement.Infrastructure.Persistence.DBOs.Base;

internal class Vehicle : BaseDbo
{
    public Guid VehicleId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public short Type { get; set; }

    public string ExternalId { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public decimal StartingBid { get; set; }

    public int? NumberOfDoors { get; set; }

    public int? NumberOfSeats { get; set; }

    public decimal? LoadCapacity { get; set; }
}
