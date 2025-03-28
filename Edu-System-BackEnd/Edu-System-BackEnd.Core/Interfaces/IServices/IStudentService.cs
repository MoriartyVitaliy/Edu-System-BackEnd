using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(Guid id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task UpdateStudentAsync(UpdateStudentDto updateStudentDto);
        Task UpdateStudentClassAsync(Guid studentId, Guid classId);
        Task DeleteStudentAsync(Guid id);
    }
}