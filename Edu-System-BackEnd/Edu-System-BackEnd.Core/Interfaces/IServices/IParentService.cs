using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface IParentService
    {
        Task<IEnumerable<ParentDto>> GetAllAsync();
        Task<ParentDto?> GetByIdAsync(Guid id);
        Task AddAsync(CreateParentDto createParentDto);
        Task UpdateAsync(UpdateParentDto updateParentDto);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<StudentDto>> GetChildrenAsync(Guid parentId);
        Task LinkChildAsync(Guid parentId, Guid studentId);
    }

}
