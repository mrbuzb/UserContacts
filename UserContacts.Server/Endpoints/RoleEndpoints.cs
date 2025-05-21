using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserContacts.Bll.Services;

namespace UserContacts.Server.Endpoints;

public static class RoleEndpoints
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/role")
            .RequireAuthorization()
            .WithTags("UserRole Management");

        userGroup.MapGet("/get-all-roles", [Authorize(Roles = "Admin, SuperAdmin")]
        async (IRoleService _roleService) =>
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Results.Ok(roles);
        })
        .WithName("GetAllUsers");

        userGroup.MapGet("/get-all-users-by-role", [Authorize(Roles = "Admin, SuperAdmin")][ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
        async (string role,IRoleService _roleService) =>
        {
            var users = await _roleService.GetAllUsersByRoleAsync(role);
            return Results.Ok(users);
        })
        .WithName("GetUsersByRole")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden)
        .Produces(StatusCodes.Status500InternalServerError);
        ;
    }
}
