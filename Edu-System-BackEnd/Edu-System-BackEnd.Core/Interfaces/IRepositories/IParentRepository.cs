using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{

    public interface IParentRepository : ICrudRepository<Parent>
    {
        Task<IEnumerable<Student>> GetChildrenAsync(Guid parentId);
        Task LinkChildAsync(Guid parentId, Guid studentId);
    }
}