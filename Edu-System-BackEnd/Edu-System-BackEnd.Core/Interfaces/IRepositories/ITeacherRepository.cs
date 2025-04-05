using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ITeacherRepository : ICrudRepository<Teacher>
    {
        public Task<IEnumerable<SchoolClass>> GetAllTeacherClassesAsync(Guid teacherId);
        public Task UpdateTeacherClassAsync(Guid teacherId, Guid classId);
        public Task DeleteTeacherClassAsync(Guid teacherId, Guid classId);
        public Task<(Teacher, SchoolClass)> GetTeacherClassAsync(Guid teacherId, Guid classId);
    }
}
