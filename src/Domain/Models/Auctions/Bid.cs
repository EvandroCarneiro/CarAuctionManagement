namespace BCA.CarAuctionManagement.Domain.Models.Auctions;

using BCA.CarAuctionManagement.Domain.Models.Base;

public class Bid : ValueObject
{
    public Bid(
        decimal amount,
        Guid userId)
    {
        CreatedDate = DateTimeOffset.UtcNow;
        Amount = amount;
        UserId = userId;
    }

    public Bid(
        DateTimeOffset createdDate,
        decimal amount,
        User user)
        : this(amount, user.Id)
    {
        CreatedDate = createdDate;
        User = user;
    }

    public DateTimeOffset CreatedDate { get; init; }

    public decimal Amount { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    internal void SetUser(User user) => User = user;
}
