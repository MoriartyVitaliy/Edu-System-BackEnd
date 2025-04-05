using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface IDailyScheduleRepository : ICrudRepository<DailySchedule>
    {
        Task AddRangeAsync(IEnumerable<DailySchedule> dailySchedules);
        Task<bool> AnyAsync(Guid weeklyScheduleId, DayOfWeek dayOfWeek);
        Task<DailySchedule?> GetByClassAndDataAsync(Guid classId, DateOnly date);
    }
}
