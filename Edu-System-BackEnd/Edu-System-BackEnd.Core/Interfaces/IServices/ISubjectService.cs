using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Subject;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
        Task<SubjectDto?> GetSubjectByIdAsync(Guid id);
        Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto createSubjectDto);
        Task UpdateSubjectAsync(UpdateSubjectDto updateSubjectDto);
        Task DeleteSubjectAsync(Guid id);
    }
}