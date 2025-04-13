using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class ParentService : IParentService
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IParentRepository _parentRepository;
        private readonly IMapper _mapper;
        public ParentService(IUserService userService , IParentRepository parentRepository, IMapper mapper, IRoleService roleService)
        {
            _parentRepository = parentRepository;
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task AddAsync(CreateParentDto createParentDto)
        {
            await _userService.AddAsync(createParentDto.User);

            var parent = await _userService.GetByEmailAsync(createParentDto.User.Email)
                ?? throw new NotFoundException($"User with email {createParentDto.User.Email} not found.");

            await _roleService.AssignUserRole(parent.Id, "Parent");

            var parentEntity = _mapper.Map<Parent>(createParentDto);

            parentEntity.UserId = parent.Id;
            parentEntity.User = null;

            await _parentRepository.AddAsync(parentEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _parentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ParentDto>> GetAllAsync()
        {
            var parents = await _parentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParentDto>>(parents);
        }

        public async Task<ParentDto?> GetByIdAsync(Guid id)
        {
            var parent = await _parentRepository.GetByIdAsync(id);
            if (parent == null)
                throw new NotFoundException($"Parent with ID {id} not found.");
            return _mapper.Map<ParentDto>(parent);
        }

        public async Task<IEnumerable<StudentDto>> GetChildrenAsync(Guid parentId)
        {
            var students = await _parentRepository.GetChildrenAsync(parentId);

            if (students == null || !students.Any())
                throw new NotFoundException("No children found for this parent");

            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task LinkChildAsync(Guid parentId, Guid studentId)
        {
            await _parentRepository.LinkChildAsync(parentId, studentId);
        }

        public async Task UpdateAsync(UpdateParentDto updateParentDto)
        {
            await _userService.UpdateAsync(updateParentDto.User);
        }
    }
}
