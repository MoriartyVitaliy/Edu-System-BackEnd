using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(IUserService userService, ITeacherRepository teacherRepository, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task AddAsync(CreateTeacherDto createTeacherDto)
        {
            await _userService.AddAsync(createTeacherDto.User);

            var user = await _userService.GetByEmailAsync(createTeacherDto.User.Email)
                ?? throw new NotFoundException($"User with email {createTeacherDto.User.Email} not found.");

            await _roleService.AssignUserRole(user.Id, "Teacher");

            var teacher = _mapper.Map<Teacher>(createTeacherDto);

            teacher.UserId = user.Id;
            teacher.User = null;

            await _teacherRepository.AddAsync(teacher);

        }

        public async Task AddClassSupervisionAsync(Guid teacherId, Guid classId)
        {
            await _teacherRepository.UpdateTeacherClassAsync(teacherId, classId);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto?> GetByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Teacher with ID {id} not found.");

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<IEnumerable<SchoolClassDto>> GetSupervisedClassesAsync(Guid teacherId)
        {
            var supervisedClasses = await _teacherRepository.GetAllTeacherClassesAsync(teacherId);
            return _mapper.Map<IEnumerable<SchoolClassDto>>(supervisedClasses);
        }

        public async Task RemoveClassSupervisionAsync(Guid teacherId, Guid classId)
        {
            await _teacherRepository.DeleteTeacherClassAsync(teacherId, classId);
        }

        public async Task UpdateAsync(UpdateTeacherDto updateTeacherDto)
        {
            await _userService.UpdateAsync(updateTeacherDto.User);

            var teacher = _mapper.Map<Teacher>(updateTeacherDto);

            await _teacherRepository.UpdateAsync(teacher);
        }
    }
}
