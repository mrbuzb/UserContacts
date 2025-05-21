namespace UserContacts.Server.Middlewares;

public class GeoBlockMiddleware
{
    private readonly RequestDelegate _next;

    public GeoBlockMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();

        // Imagine you check country using an IP lookup service
        var isBlockedCountry = ip?.StartsWith("::10") == true;

        if (isBlockedCountry)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access denied from your country 🌍");
            return;
        }

        await _next(context);
    }
}

