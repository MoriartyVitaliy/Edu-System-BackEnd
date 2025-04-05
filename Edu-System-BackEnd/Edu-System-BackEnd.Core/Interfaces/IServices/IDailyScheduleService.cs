using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface IDailyScheduleService
    {
        Task<IEnumerable<DailyScheduleDto>> GetAllDailySchedulesAsync();
        Task<DailyScheduleDto?> GetDailyScheduleByIdAsync(Guid dailyScheduleId);
        Task<DailyScheduleDto> CreateDailyScheduleAsync(CreateDailyScheduleDto createDailyScheduleDto);
        Task UpdateDailyScheduleAsync(UpdateDailyScheduleDto updateDailyScheduleDto);
        Task DeleteDailyScheduleAsync(Guid dailyScheduleId);
        Task<DailyScheduleDto?> GetDailySchedulesByClassAndDateAsync(Guid classId, DateOnly date);
        Task<DailyScheduleDto?> GetDailySchedulesByClassTodayAsync(Guid classId);
    }
}
