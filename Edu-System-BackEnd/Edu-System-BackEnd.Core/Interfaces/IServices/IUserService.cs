using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<UserDto?> GetByEmailAsync(string email);
        Task AddAsync(CreateUserDto createUserDto);
        Task UpdateAsync(UpdateUserDto updateUserDto);
        Task DeleteAsync(Guid id);
        Task UpdatePassword(Guid id, string passwordHash);
    }

}
