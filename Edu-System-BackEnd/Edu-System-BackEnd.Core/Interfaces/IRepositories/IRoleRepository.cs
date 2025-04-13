using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface IRoleRepository : ICrudRepository<Role>
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(Guid userId);
        Task<bool> IsUserInRoleAsync(Guid userId, string roleName);
        Task<bool> AssignUserRoleAsync(Guid userId, Role role);
        Task<bool> RemoveUserRoleAsync(Guid userId, Role role);
        Task<bool> IsRoleExist(string roleName);
    }
}
