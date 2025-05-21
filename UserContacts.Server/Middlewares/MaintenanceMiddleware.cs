namespace UserContacts.Server.Middlewares;

public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public MaintenanceMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task Invoke(HttpContext context)
    {
        var maintenanceMode = _config.GetValue<bool>("AppSettings:Maintenance");

        if (maintenanceMode)
        {
            context.Response.StatusCode = 503; // Service Unavailable
            await context.Response.WriteAsJsonAsync(new { message = "The site is under maintenance. 🛠️" });
            return;
        }

        await _next(context);
    }
}

