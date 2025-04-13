using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface IWeeklyScheduleRepository : ICrudRepository<WeeklySchedule>
    {
        Task<IEnumerable<WeeklySchedule?>> GetAllWeeklySchedulesByClassId(Guid schoolClassId);
        Task<WeeklySchedule?> GetCurrentWeekScheduleAsync(Guid schoolClassId, DateTime startOfWeek);
        Task<bool> ExistsForWeekAsync(Guid schoolClassId, DateTime startOfWeek);
    }
}
