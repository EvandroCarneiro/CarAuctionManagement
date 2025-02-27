namespace BCA.CarAuctionManagement.Api.Middleware;

using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class ExceptionMiddleware
{
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(context, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception ex)
    {
        var errorModel = new ErrorModel
        {
            ExpectedStatusCode = context.Response.StatusCode,
            Message = ex.Message,
            DeveloperMessage = ex.StackTrace?.ToString(),
        };

        var serializedError = JsonSerializer.Serialize(errorModel, jsonSerializerOptions);
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(serializedError, Encoding.UTF8);
    }
}

internal class ErrorModel
{
    public int ExpectedStatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? DeveloperMessage { get; set; }
}
