using System.Diagnostics;

namespace UserContacts.Server.Middlewares;

public class RequestDurationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestDurationMiddleware> _logger;

    public RequestDurationMiddleware(RequestDelegate next, ILogger<RequestDurationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context); // proceed to next middleware/controller

        stopwatch.Stop();

        var duration = stopwatch.ElapsedMilliseconds / 1000.0;

        var method = context.Request.Method;
        var path = context.Request.Path;
        var statusCode = context.Response.StatusCode;

        _logger.LogInformation("Request [{Method}] {Path} completed in {Duration} secunds with status {StatusCode}",
            method, path, duration, statusCode);
    }
}
