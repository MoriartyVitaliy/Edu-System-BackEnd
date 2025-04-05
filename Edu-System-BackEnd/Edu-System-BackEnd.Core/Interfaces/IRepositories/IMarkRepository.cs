using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface IMarkRepository : ICrudRepository<Mark>
    {
        public Task<IEnumerable<Mark>> GetMarksByLessonIdAsync(Guid lessonId);
        public Task<IEnumerable<Mark>> GetMarksByStudentIdAsync(Guid studentId);
    }
}
