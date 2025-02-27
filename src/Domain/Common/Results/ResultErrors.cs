namespace BCA.CarAuctionManagement.Domain.Common.Results;

using FluentResults;

internal static class ResultErrors
{
    public static Error NotFound(string message) => new Error(message).WithMetadata("type", "not.found");

    public static Error Conflict(string message) => new Error(message).WithMetadata("type", "conflict");
}
