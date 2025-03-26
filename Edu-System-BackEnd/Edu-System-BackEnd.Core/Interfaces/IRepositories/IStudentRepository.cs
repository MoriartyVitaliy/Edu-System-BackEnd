using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface IStudentRepository : ICrudRepository<Student>
    {
        //ToDo : Add custom methods
        public Task UpdateStudentClassAsync(Guid studentId, Guid classId);
    }
}