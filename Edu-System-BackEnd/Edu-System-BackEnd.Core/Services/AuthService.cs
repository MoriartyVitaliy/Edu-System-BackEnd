using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using System.Security.Claims;
using System.Text;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Login;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly ILoginAttemptProvider _loginAttemptProvider;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration config,
            IPasswordHasher<object> passwordHasher,
            ILoginAttemptProvider loginAttemptProvider)
        {
            _userRepository = userRepository;
            _config = config;
            _passwordHasher = passwordHasher;
            _loginAttemptProvider = loginAttemptProvider;
        }
        public async Task<string> LoginAsync(LoginDto login)
        {
            if (login.Email == null || login.Password == null)
                throw new ArgumentNullException("Email and Password cannot be null.");

            if (await _loginAttemptProvider.IsAccountLockedAsync(login.Email))
                throw new UnauthorizedAccessException("Your account is temporarily locked due to multiple failed login attempts.");


            var user = await _userRepository.GetByEmailAsync(login.Email);

            if (user == null)
            {
                await _loginAttemptProvider.RecordFailedLoginAsync(login.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(new object(), user.PasswordHash, login.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                await _loginAttemptProvider.RecordFailedLoginAsync(login.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            await _loginAttemptProvider.RecordSuccessfulLoginAsync(login.Email);


            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
            };

            // Додаємо ролі:
            foreach (var role in user.UserRoles.Select(r => r.Role.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}