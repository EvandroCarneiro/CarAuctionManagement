namespace BCA.CarAuctionManagement.Domain.Models.Base;

using System.Collections.Generic;

public class PagedResult<T>(
    ICollection<T> entries,
    int pageNumber,
    int totalItems)
{
    public int PageNumber { get; } = pageNumber;

    public int TotalItems { get; } = totalItems;

    public IEnumerable<T> Entries { get; } = entries;

    public int TotalPages { get; } = entries.Count > 0 ? (int)Math.Ceiling((double)totalItems / entries.Count) : 0;
}