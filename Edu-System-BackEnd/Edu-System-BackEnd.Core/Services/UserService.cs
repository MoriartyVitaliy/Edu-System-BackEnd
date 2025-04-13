using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher<object> passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateUserDto createUserDto)
        {
            var user = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (user != null)
                throw new Exception("User already exists");

            if (!PasswordValidator.IsValidPassword(createUserDto.Password))
                throw new BadRequestException("Password must be at least 7 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.");

            var passwordHash = _passwordHasher.HashPassword(new object(), createUserDto.Password);
            var newUser = _mapper.Map<User>(createUserDto);

            newUser.PasswordHash = passwordHash;

            await _userRepository.AddAsync(newUser);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                throw new NotFoundException("User not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)                           
                throw new NotFoundException("User not found");
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(updateUserDto.Id);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            _mapper.Map(updateUserDto, user);

            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdatePassword(Guid id, string passwordHash)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }
            await _userRepository.UpdatePassword(id, passwordHash);
        }
    }
}
