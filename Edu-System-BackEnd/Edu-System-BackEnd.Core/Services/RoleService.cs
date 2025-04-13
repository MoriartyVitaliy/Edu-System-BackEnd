using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Role;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using StackExchange.Redis;
using Role = Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Role;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> AssignUserRole(Guid userId, string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);

            if (role == null)
                throw new NotFoundException($"Role '{roleName}' not found.");

            if(await _roleRepository.IsUserInRoleAsync(userId, roleName))
                throw new AlreadyExistsException($"User '{userId}' already has the role '{roleName}'.");

            return await _roleRepository.AssignUserRoleAsync(userId, role);
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            if(await _roleRepository.IsRoleExist(createRoleDto.Name))
                throw new AlreadyExistsException($"Role '{createRoleDto.Name}' already exists.");
            
            var role = _mapper.Map<Role>(createRoleDto);
            role.Id = Guid.NewGuid();

            await _roleRepository.AddAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task DeleteRoleAsyncById(Guid roleId)
        {
            await _roleRepository.DeleteAsync(roleId);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetRoleByIdAsync(Guid roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId)
                ??  throw new NotFoundException($"Role with ID {roleId} not found.");

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName)
                ?? throw new NotFoundException($"Role with name {roleName} not found.");
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesByUserIdAsync(Guid userId)
        {
            var roles = await _roleRepository.GetRolesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<bool> RemoveUserRoleAsync(Guid userId, string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);

            if (role == null)
                throw new NotFoundException($"Role '{roleName}' not found.");

            return await _roleRepository.RemoveUserRoleAsync(userId, role);
        }

        public async Task UpdateRoleAsync(UpdateRoleDto roleDto)
        {
            if (await _roleRepository.IsRoleExist(roleDto.Name))
                throw new AlreadyExistsException($"Role with name '{roleDto.Name}' already exists.");
            

            var role = _mapper.Map<Role>(roleDto);

            await _roleRepository.UpdateAsync(role);
        }
    }
}
