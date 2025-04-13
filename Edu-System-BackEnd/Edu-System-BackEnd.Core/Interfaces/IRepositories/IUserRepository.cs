using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<bool> UpdatePassword(Guid user, string passwordHash);
        Task<User?> GetByEmailAsync(string email);
    }
}
