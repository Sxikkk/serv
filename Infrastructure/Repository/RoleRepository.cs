using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RoleRepository: IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Role> GetUserRoleByIdAsync(int roleId)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.id == roleId);
        return role;
    }

    public async Task<string> GetRoleNameByIdAsync(int roleId)
    {
        var role = await GetUserRoleByIdAsync(roleId);
        return role.Name;
    }
}