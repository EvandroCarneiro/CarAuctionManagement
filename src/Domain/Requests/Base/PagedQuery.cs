namespace BCA.CarAuctionManagement.Domain.Requests.Base;

using BCA.CarAuctionManagement.Domain.Models.Base;

public abstract class PagedQuery<TResponse>(PagedFilter pagedFilter) : Query<TResponse>
    where TResponse : FluentResults.ResultBase
{
    public PagedFilter PagedFilter { get; } = pagedFilter;
}
