using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task CreateStudentAsync(Student studentDto);
        Task UpdateStudentAsync(Student student);
        Task UpdateStudentClassAsync(Guid studentId, Guid classId);
        Task DeleteStudentAsync(Guid id);
    }
}