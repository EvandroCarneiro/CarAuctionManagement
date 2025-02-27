namespace BCA.CarAuctionManagement.Infrastructure.Persistence.DBOs;

using System;

internal class User
{
    public Guid UserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public string Name { get; }
}
