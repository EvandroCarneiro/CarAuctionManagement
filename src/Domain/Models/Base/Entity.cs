namespace BCA.CarAuctionManagement.Domain.Models.Base;

public abstract class Entity<TId>()
{
    public TId Id { get; protected init; }

    public DateTimeOffset CreatedDate { get; protected init; }

    #region BaseBehaviours

    public override bool Equals(object obj)
    {
        if (obj is not Entity<TId> compareTo)
        {
            return false;
        }

        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }

        return Id.Equals(compareTo.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
    }

    #endregion
}
