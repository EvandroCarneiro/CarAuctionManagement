namespace BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal static class BaseQueries
{
    public const string GetPagedBaseQuery = @"
		;WITH QueryResult AS 
		(
			{0}
		), 
		TotalCount AS
		( 
			SELECT 
				COUNT(*) AS TotalItems
			FROM 
				QueryResult 
		)
		SELECT *
		FROM QueryResult, TotalCount
		{1}
		OFFSET @PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY";

    public const string OrderBy1Query = "ORDER BY 1";
}
