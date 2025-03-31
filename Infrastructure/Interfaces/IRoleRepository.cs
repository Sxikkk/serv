using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IRoleRepository
{ 
    Task<Role> GetUserRoleByIdAsync(int roleId);
    Task<string> GetRoleNameByIdAsync(int roleId);
}