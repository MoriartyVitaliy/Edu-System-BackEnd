using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }
        public async Task<TeacherDto?> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Teacher with id {id} not found");

            return _mapper.Map<TeacherDto>(teacher);
        }
        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto createTeacherDto)
        {
            var teacher = _mapper.Map<Teacher>(createTeacherDto);
            teacher.Id = Guid.NewGuid();

            await _teacherRepository.AddAsync(teacher);
            return _mapper.Map<TeacherDto>(teacher);
        }
        public async Task UpdateTeacherAsync(UpdateTeacherDto updateTeacherDto)
        {
            var teacher = await _teacherRepository.GetByIdAsync(updateTeacherDto.Id);
            if (teacher == null)
                throw new NotFoundException($"Teacher with ID {updateTeacherDto.Id} not found.");

            _mapper.Map(updateTeacherDto, teacher);
            await _teacherRepository.UpdateAsync(teacher);
        }
        public async Task DeleteTeacherAsync(Guid id)
        {
            await _teacherRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<SchoolClassDto>> GetTeacherClassesAsync(Guid teacherId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(teacherId)
                ?? throw new NotFoundException($"Teacher with id {teacherId} not found");
            return _mapper.Map<IEnumerable<SchoolClassDto>>(teacher.ClassSupervisions);
        }
        public async Task UpdateTeacherClassAsync(Guid teacherId, Guid classId)
        {
            await _teacherRepository.UpdateTeacherClassAsync(teacherId, classId);
        }
        public async Task DeleteTeacherClassAsync(Guid teacherId, Guid subjectId)
        {
            await _teacherRepository.DeleteTeacherClassAsync(teacherId, subjectId);
        }
    }
}
