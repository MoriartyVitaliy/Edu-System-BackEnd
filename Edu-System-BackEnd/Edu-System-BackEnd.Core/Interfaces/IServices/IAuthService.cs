using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Login;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto login);
    }
}
