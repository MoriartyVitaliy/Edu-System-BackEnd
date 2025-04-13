using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface IWeeklyScheduleService
    {
        Task<IEnumerable<WeeklyScheduleDto>> GetAllWeeklySchedulesAsync();
        Task<WeeklyScheduleDto?> GetWeeklyScheduleByIdAsync(Guid weeklyScheduleId);
        Task<WeeklyScheduleDto> CreateWeeklyScheduleAsync(CreateWeeklyScheduleDto createWeeklyScheduleDto);
        Task UpdateWeeklyScheduleAsync(UpdateWeeklyScheduleDto updateWeeklyScheduleDto);
        Task DeleteWeeklyScheduleAsync(Guid weeklyScheduleId);
        Task<WeeklyScheduleDto> CreateNextWeekScheduleAsync(Guid schoolClassId);
        Task<WeeklyScheduleDto?> GetCurrentWeekScheduleAsync(Guid schoolClassId);
        Task<IEnumerable<WeeklyScheduleDto>> GetAllWeeklySchedulesByClassId(Guid schoolClassId);
        Task<IEnumerable<WeeklyScheduleDto>> GetAllWeeklySchedulesByClassName(string className);
    }
}
