using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ITeacherRepository : ICrudRepository<Teacher>
    {
        //ToDo: Add custom methods
        public Task UpdateTeacherClassAsync(Guid teacherId, Guid classId);
    }
}
