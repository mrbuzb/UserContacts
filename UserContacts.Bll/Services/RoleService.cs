using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Caching.Memory;
using UserContacts.Bll.Dtos;
using UserContacts.Dal.Entities;
using UserContacts.Repository.Services;

namespace UserContacts.Bll.Services;

public class RoleService(IRoleRepository _roleRepo,IMemoryCache _cache) : IRoleService
{

    private const string _cacheKey = "roles_list";
    private RoleGetDto Converter(UserRole role)
    {
        return new RoleGetDto
        {
            Description = role.Description,
            Id = role.Id,
            Name = role.Name,
        };
    }

    private UserGetDto Converter(User user)
    {
        return new UserGetDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserId = user.UserId,
            UserName = user.UserName,
            Role = user.Role.Name,
        };
    }
    public async Task<List<RoleGetDto>> GetAllRolesAsync()
    {
        List<UserRole> roles;
        if (_cache.TryGetValue(_cacheKey, out List<UserRole> cachedRoles))
        {
            roles = cachedRoles!;
        }
        else
        {
            roles = await _roleRepo.GetAllRolesAsync();
        }



        _cache.Set(_cacheKey, roles, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20),
            SlidingExpiration = TimeSpan.FromMinutes(15)
        });

        return roles.Select(Converter).ToList();
    }

    public async Task<long> GetRoleIdAsync(string role)
    {
        return await _roleRepo.GetRoleIdAsync(role);
    }

    public async Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role)
    {
        var users = await _roleRepo.GetAllUsersByRoleAsync(role);
        return users.Select(Converter).ToList();
    }
}
