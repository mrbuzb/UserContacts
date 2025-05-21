namespace UserContacts.Server.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "X-Api-Key";
    private readonly string _expectedKey = "super-secret-key"; // put in config in real apps

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var providedKey) ||
            providedKey != _expectedKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Invalid API key 🔐" });
            return;
        }

        await _next(context);
    }
}

