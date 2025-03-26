using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(Guid id);
        Task CreateTeacherAsync(Teacher teacherDto);
        Task UpdateTeacherAsync(Teacher teacher);
        Task UpdateTeacherClassAsync(Guid teacherId, Guid classId);
        Task DeleteTeacherAsync(Guid id);
    }
}
