using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;

        public AdminService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        public async Task InitializeAdminAsync()
        {
            string adminEmail = _configuration["Admin:Email"];
            string adminPassword = _configuration["Admin:Password"];

            var existingUser = await _userRepository.GetByEmailAsync(adminEmail);
            if (existingUser != null) return;

            var adminRole = await _roleRepository.GetRoleByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
                await _roleRepository.AddAsync(adminRole);
            }

            var passwordHasher = new PasswordHasher<User>();
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "Admin",
                MiddleName = "Admin",
                PasswordHash = passwordHasher.HashPassword(null, adminPassword),
                UserRoles = new List<UserRole>()
                {
                    new UserRole
                    {
                        RoleId = adminRole.Id
                    }
                }
            };

            await _userRepository.AddAsync(newUser);
        }
    }
}
