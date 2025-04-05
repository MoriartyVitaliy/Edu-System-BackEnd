using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    // ToDo: Add ICrudRepository
    public interface ILessonRepository : ICrudRepository<Lesson>
    {
        Task<List<Lesson>> GetLessonsByScheduleIdAsync(Guid scheduleId);
    }
}