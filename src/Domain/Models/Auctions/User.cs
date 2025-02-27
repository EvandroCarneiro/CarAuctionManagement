namespace BCA.CarAuctionManagement.Domain.Models.Auctions;

using System;

using BCA.CarAuctionManagement.Domain.Models.Base;

public class User : Entity<Guid>
{
    public User(string name)
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTimeOffset.UtcNow;
        Name = name;
    }

    public User(
        Guid id,
        DateTimeOffset createdDate,
        string name)
        : this(name)
    {
        Id = id;
        CreatedDate = createdDate;
        Name = name;
    }

    public string Name { get; private set; }
}
