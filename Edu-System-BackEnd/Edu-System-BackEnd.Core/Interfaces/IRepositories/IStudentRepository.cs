using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface IStudentRepository : ICrudRepository<Student>
    {
        Task UpdateClassAsync(Guid studentId, Guid newClassId);
        Task AddParentAsync(Guid studentId, Guid parentId);
    }

}