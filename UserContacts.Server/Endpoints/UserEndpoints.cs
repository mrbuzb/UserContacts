using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UserContacts.Bll.Services;
using UserContacts.Dal.Entities;


namespace UserContacts.Server.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/user")
            .RequireAuthorization()
            .WithTags("User Management");

        userGroup.MapDelete("/delete", [Authorize(Roles = "Admin, SuperAdmin")]
        async (long userId, HttpContext httpContext, IUserService userService) =>
        {
            var role = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            await userService.DeleteUserByIdAsync(userId, role);
            return Results.Ok();
        })
        .WithName("DeleteUser");



        userGroup.MapPatch("/updateRole", [Authorize(Roles = "SuperAdmin")]
        async (long userId, string userRole, IUserService userService) =>
        {
            await userService.UpdateUserRoleAsync(userId, userRole);
            return Results.Ok();
        })
        .WithName("UpdateUserRole");

    }
}
