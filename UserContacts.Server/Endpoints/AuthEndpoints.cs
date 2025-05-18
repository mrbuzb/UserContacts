using UserContacts.Bll.Dtos;
using UserContacts.Bll.Services;

namespace UserContacts.Server.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/auth")
            .WithTags("Authentication");

        userGroup.MapPost("/sign-up",
        async (UserCreateDto user,IAuthService _service) =>
        {
            return Results.Ok(await _service.SignUpUserAsync(user));
        })
        .WithName("SignUp");

        userGroup.MapPost("/login",
        async (UserLoginDto user,IAuthService _service) =>
        {
            return Results.Ok(await _service.LoginUserAsync(user));
        })
        .WithName("Login");

        userGroup.MapPost("/refresh-token",
        async (RefreshRequestDto refresh,IAuthService _service) =>
        {
            return Results.Ok(await _service.RefreshTokenAsync(refresh));
        })
        .WithName("RefreshToken");
    }
}
