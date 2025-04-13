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
    public class StudentService : IStudentService
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentService(IUserService userService, IStudentRepository studentRepository, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task AddAsync(CreateStudentDto createStudentDto)
        {
            await _userService.AddAsync(createStudentDto.User);

            var student = await _userService.GetByEmailAsync(createStudentDto.User.Email)
                ?? throw new NotFoundException($"User with email {createStudentDto.User.Email} not found.");

            await _roleService.AssignUserRole(student.Id, "Student");

            var studentEntity = _mapper.Map<Student>(createStudentDto);
            
            studentEntity.UserId = student.Id;
            studentEntity.User = null;

            await _studentRepository.AddAsync(studentEntity);
        }

        public async Task AddOrUpdateParentAsync(Guid studentId, Guid parentId)
        {
            await _studentRepository.AddParentAsync(studentId, parentId);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto?> GetByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with ID {id} not found.");

            return _mapper.Map<StudentDto>(student);
        }

        public async Task UpdateAsync(UpdateStudentDto updateStudentDto)
        {
            await _userService.UpdateAsync(updateStudentDto.User);

            var student = _mapper.Map<Student>(updateStudentDto);

            await _studentRepository.UpdateAsync(student);
        }

        public Task UpdateClassAsync(Guid studentId, Guid newClassId)
        {
            return _studentRepository.UpdateClassAsync(studentId, newClassId);
        }
    }
}
