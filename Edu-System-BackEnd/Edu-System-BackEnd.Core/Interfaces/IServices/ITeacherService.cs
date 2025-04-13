using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto?> GetByIdAsync(Guid id);
        Task AddAsync(CreateTeacherDto createTeacherDto);
        Task UpdateAsync(UpdateTeacherDto updateTeacherDto);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<SchoolClassDto>> GetSupervisedClassesAsync(Guid teacherId);
        Task AddClassSupervisionAsync(Guid teacherId, Guid classId);
        Task RemoveClassSupervisionAsync(Guid teacherId, Guid classId);
    }
}
