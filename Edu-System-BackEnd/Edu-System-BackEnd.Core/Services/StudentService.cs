using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            var students = await _studentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with id {id} not found");
            
            return _mapper.Map<StudentDto>(students);
        }
        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            var student = _mapper.Map<Student>(createStudentDto);
            student.Id = Guid.NewGuid();

            await _studentRepository.AddAsync(student);
            return _mapper.Map<StudentDto>(student);
        }
        public async Task UpdateStudentAsync(UpdateStudentDto updateStudentDto)
        {
            var student = await _studentRepository.GetByIdAsync(updateStudentDto.Id);
            if (student == null)
                throw new NotFoundException($"Student with ID {updateStudentDto.Id} not found.");

            // Маппим только разрешённые поля
            _mapper.Map(updateStudentDto, student);

            await _studentRepository.UpdateAsync(student);
        }
        public async Task UpdateStudentClassAsync(Guid studentId, Guid classId)
        {
            await _studentRepository.UpdateStudentClassAsync(studentId, classId);
        }
        public async Task DeleteStudentAsync(Guid id)
        {
            await _studentRepository.DeleteAsync(id);
        }
    }
}
