using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto?> GetTeacherByIdAsync(Guid id);
        Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto createTeacherDto);
        Task UpdateTeacherAsync(UpdateTeacherDto updateTeacherDto);
        Task DeleteTeacherAsync(Guid id);
        public Task<IEnumerable<SchoolClassDto>> GetTeacherClassesAsync(Guid teacherId);
        Task UpdateTeacherClassAsync(Guid teacherId, Guid classId);
        Task DeleteTeacherClassAsync(Guid teacherId, Guid classId);
    }
}
