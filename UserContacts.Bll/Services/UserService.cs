using Microsoft.EntityFrameworkCore;
using UserContacts.Core.Errors;
using UserContacts.Dal;
using UserContacts.Repository.Services;

namespace UserContacts.Bll.Services;

public class UserService(IUserRepository UserRepository, MainContext _context) : IUserService
{
    public async Task DeleteUserByIdAsync(long userId, string userRole)
    {
        if (userRole == "SuperAdmin")
        {
            await UserRepository.DeleteUserByIdAsync(userId);
        }
        else if (userRole == "Admin")
        {
            var user = await UserRepository.SelectUserByIdAync(userId);
            if (user.Role.Name == "User")
            {
                await UserRepository.DeleteUserByIdAsync(userId);
            }
            else
            {
                throw new NotAllowedException("Admin can not delete Admin or SuperAdmin");
            }
        }
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        await UserRepository.UpdateUserRoleAsync(userId, userRole);
    }
}
