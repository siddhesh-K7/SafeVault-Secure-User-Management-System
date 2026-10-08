using System.Net;
using System.Text.Json;
namespace SafeVault.Middleware;

public class SecurityExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SecurityExceptionMiddleware> _logger;

    public SecurityExceptionMiddleware(RequestDelegate next, ILogger<SecurityExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Add Security Headers
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'");

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(context);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = "An unexpected error occurred. Please try again later." });
        return context.Response.WriteAsync(result);
    }
}