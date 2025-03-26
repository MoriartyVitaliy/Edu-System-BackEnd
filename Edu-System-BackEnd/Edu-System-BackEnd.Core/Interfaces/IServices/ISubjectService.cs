using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject?> GetSubjectByIdAsync(Guid id);
        Task CreateSubjectAsync(Subject subjectDto);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(Guid id);
    }
}