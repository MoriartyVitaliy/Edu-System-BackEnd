using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(Guid id);
        Task AddAsync(CreateStudentDto createStudentDto);
        Task UpdateAsync(UpdateStudentDto updateStudentDto);
        Task DeleteAsync(Guid id);

        Task UpdateClassAsync(Guid studentId, Guid newClassId);
        Task AddOrUpdateParentAsync(Guid studentId, Guid parentId);
    }

}