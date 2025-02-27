namespace BCA.CarAuctionManagement.Domain.Models.Base;

using Constants = Domain.Common.Constants;

public class PagedFilter(int? pageNumber, int? pageSize)
{
    public int PageNumber { get; private set; } = pageNumber ?? Constants.Pagination.PageNumber;

    public int PageSize { get; private set; } = pageSize ?? Constants.Pagination.PageSize;
}
