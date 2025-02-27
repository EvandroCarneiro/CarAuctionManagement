namespace BCA.CarAuctionManagement.Api.Extensions;

using BCA.CarAuctionManagement.Api.Dto;

using FluentResults;

using Microsoft.AspNetCore.Mvc;

public static class ResultExtensions
{
    public static IActionResult AsOkResult(this Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.IsSuccess ? new OkResult() : result.GetErrors();
    }

    public static IActionResult AsOkResult<T>(this Result<T> result, Func<BaseDto> mapToDto)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(mapToDto);

        var dto = result.ValueOrDefault is not null ? mapToDto() : null;

        return result.IsSuccess
            ? new OkObjectResult(dto)
            : result.GetErrors();
    }

    public static IActionResult AsCreatedAtRouteResult<T>(this Result<T> result, string routeName, object routeValues)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(routeName);
        ArgumentNullException.ThrowIfNull(routeValues);

        return result.IsSuccess
            ? new CreatedAtRouteResult(routeName, routeValues, null)
            : result.GetErrors();
    }

    private static IActionResult AsBadRequestResult(this ResultBase result)
    {
        var errorMessages = result.Errors.Select(x => x.Message);

        return new BadRequestObjectResult(errorMessages);
    }

    private static IActionResult AsNotFoundResult(string message) => new NotFoundObjectResult(message);

    private static IActionResult AsConflictResult(string message) => new ConflictObjectResult(message);

    private static IActionResult GetErrors(this ResultBase result)
    {
        var errorType = result.Errors.Find(error => error.HasMetadataKey("type"));

        if (errorType is null)
        {
            return result.AsBadRequestResult();
        }

        var actionResult = errorType switch
        {
            var error when error.Metadata.ContainsValue("not.found") => AsNotFoundResult(error.Message),
            var error when error.Metadata.ContainsValue("conflict") => AsConflictResult(error.Message),
            _ => result.AsBadRequestResult()
        };

        return actionResult;
    }
}
