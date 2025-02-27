namespace BCA.CarAuctionManagement.Api.Mappers.Base;

using Domain = Domain.Models.Base;
using Dto = Api.Dto;

public static class PagedResultMapper
{
    public static Dto.PagedResult<TDto> Map<TDomain, TDto>(
        this Domain.PagedResult<TDomain> source, 
        Func<IEnumerable<TDomain>, IEnumerable<TDto>> mapToDto)
        where TDto : Dto.BaseDto
    {
        ArgumentNullException.ThrowIfNull(mapToDto);

        return new Dto.PagedResult<TDto>(
            source.PageNumber,
            source.TotalPages,
            source.TotalItems,
            mapToDto(source.Entries));
    }
}
