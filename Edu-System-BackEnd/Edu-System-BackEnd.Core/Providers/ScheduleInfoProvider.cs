using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Providers
{
    public class ScheduleInfoProvider : IScheduleInfoProvider
    {
        private readonly IWeeklyScheduleRepository _weeklyScheduleRepository;
        private readonly IDailyScheduleRepository _dailyScheduleRepository;

        public ScheduleInfoProvider(IWeeklyScheduleRepository weeklyScheduleRepository, IDailyScheduleRepository dailyScheduleRepository)
        {
            _weeklyScheduleRepository = weeklyScheduleRepository;
            _dailyScheduleRepository = dailyScheduleRepository;
        }


        public async Task CreateDailySchedulesForWeekAsync(Guid weeklyScheduleId, Guid schoolClassId, DateTime startOfWeek)
        {
            var dailySchedules = Enumerable.Range(0, 7)
                .Select(i => new DailySchedule
                {
                    Id = Guid.NewGuid(),
                    Date = DateOnly.FromDateTime(startOfWeek.AddDays(i)),
                    WeeklyScheduleId = weeklyScheduleId,
                    SchoolClassId = schoolClassId
                })
                .ToList();

            await _dailyScheduleRepository.AddRangeAsync(dailySchedules);
        }

        public async Task<WeeklySchedule> GetWeeklyScheduleByIdAsync(Guid weeklyScheduleId)
        {
            var schedule = await _weeklyScheduleRepository.GetByIdAsync(weeklyScheduleId)
                ?? throw new NotFoundException($"Weekly schedule with ID {weeklyScheduleId} not found.");

            return schedule;
        }
    }
}
