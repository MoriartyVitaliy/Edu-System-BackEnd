using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Role;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto?> GetRoleByIdAsync(Guid roleId);
        Task<RoleDto?> GetRoleByNameAsync(string roleName);
        Task<IEnumerable<RoleDto>> GetRolesByUserIdAsync(Guid userId);
        Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task UpdateRoleAsync(UpdateRoleDto roleDto);
        Task DeleteRoleAsyncById(Guid roleId);
        Task<bool> AssignUserRole(Guid userId, string roleName);
        Task<bool> RemoveUserRoleAsync(Guid userId, string roleName);
    }
}
