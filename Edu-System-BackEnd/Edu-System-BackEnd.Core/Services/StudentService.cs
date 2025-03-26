using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }
        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }
        public async Task CreateStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
        }
        public async Task UpdateStudentAsync(Student student)
        {
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
