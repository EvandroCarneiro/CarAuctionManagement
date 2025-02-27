namespace BCA.CarAuctionManagement.Domain.Requests.Base;

public abstract class Command<TResponse> : Request<TResponse>
    where TResponse : FluentResults.ResultBase
{
}
