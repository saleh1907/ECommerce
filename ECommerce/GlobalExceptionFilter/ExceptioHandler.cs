using System.Net;
using System.Text.Json;

namespace ECommerce.GlobalExceptionFilter;

public class ExceptioHandler
{
    private readonly RequestDelegate _next;

    public ExceptioHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.BadRequest
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
