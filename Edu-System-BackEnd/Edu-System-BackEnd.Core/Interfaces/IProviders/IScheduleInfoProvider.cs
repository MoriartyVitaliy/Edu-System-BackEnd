using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders
{
    public interface IScheduleInfoProvider
    {
        Task CreateDailySchedulesForWeekAsync(Guid id, Guid schoolClassId, DateTime startOfNextWeek);
        Task<WeeklySchedule> GetWeeklyScheduleByIdAsync(Guid weeklyScheduleId);
    }
}
