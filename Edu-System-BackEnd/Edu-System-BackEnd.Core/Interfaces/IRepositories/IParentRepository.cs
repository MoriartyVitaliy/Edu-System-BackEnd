using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface IParentRepository : ICrudRepository<Parent>
    {
        //ToDo: Add custom methods
        public Task<IEnumerable<Student>> GetParentStudents(Guid parentId);
        public Task UpdateStudentToParent(Guid parentId, Guid studentId);
    }
}
