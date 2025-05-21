namespace UserContacts.Server.Middlewares;

public class NightBlockMiddleware
{
    private readonly RequestDelegate _next;

    public NightBlockMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var currentHour = DateTime.Now.Hour;

        // Block requests after 10 PM
        if (currentHour >= 12 || context.Request.Path.ToString().Contains("get"))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(new
            {
                message = "The API is closed after 10 PM. Come back tomorrow! 🌙"
            });

            return; 
        }

        await _next(context); 
    }
}
