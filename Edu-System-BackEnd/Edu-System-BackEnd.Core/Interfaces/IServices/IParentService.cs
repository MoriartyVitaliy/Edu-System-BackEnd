using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface IParentService
    {
        Task<IEnumerable<ParentDto>> GetAllParentsAsync();
        Task<ParentDto> GetParentByIdAsync(Guid id);
        Task<ParentDto> CreateParentAsync(CreateParentDto createParentDto);
        Task UpdateParentAsync(UpdateParentDto updateparentDto);
        Task DeleteParentAsync(Guid id);
        Task<IEnumerable<StudentDto>> GetParentStudentAsync(Guid parentId);
        Task UpdateParentStudentAsync(Guid parentId, Guid studentId);
    }
}
