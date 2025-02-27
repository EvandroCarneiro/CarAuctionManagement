namespace BCA.CarAuctionManagement.Domain.Requests.Base;

public abstract class Query<TResponse> : Request<TResponse>
    where TResponse : FluentResults.ResultBase
{
}
