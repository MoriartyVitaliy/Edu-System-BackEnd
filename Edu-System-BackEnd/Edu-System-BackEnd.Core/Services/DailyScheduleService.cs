using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class DailyScheduleService : IDailyScheduleService
    {
        private readonly IDailyScheduleRepository _dailyScheduleRepository;
        private readonly IScheduleInfoProvider _scheduleProvider;
        private readonly IMapper _mapper;
        public DailyScheduleService(IDailyScheduleRepository scheduleRepository, IScheduleInfoProvider scheduleInfoProvider, IMapper mapper)
        {
            _dailyScheduleRepository = scheduleRepository;
            _scheduleProvider = scheduleInfoProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DailyScheduleDto>> GetAllDailySchedulesAsync()
        {
            var dailySchedules = await _dailyScheduleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DailyScheduleDto>>(dailySchedules);
        }

        public async Task<DailyScheduleDto?> GetDailyScheduleByIdAsync(Guid dailyScheduleId)
        {
            var dailySchedule = await _dailyScheduleRepository.GetByIdAsync(dailyScheduleId);
            return _mapper.Map<DailyScheduleDto?>(dailySchedule);
        }
        public async Task<DailyScheduleDto> CreateDailyScheduleAsync(CreateDailyScheduleDto createDailyScheduleDto)
        {
            var weeklySchedule = await _scheduleProvider.GetWeeklyScheduleByIdAsync(createDailyScheduleDto.WeeklyScheduleId);
            
            if(weeklySchedule.DailySchedules.Count >= 7)
                throw new BadRequestException($"Weekly schedule with ID {weeklySchedule.Id} already has 7 daily schedules.");

            var dailySchedule = _mapper.Map<DailySchedule>(createDailyScheduleDto);

            dailySchedule.Id = Guid.NewGuid();
            dailySchedule.WeeklyScheduleId = weeklySchedule.Id;
            
            await _dailyScheduleRepository.AddAsync(dailySchedule);
            return _mapper.Map<DailyScheduleDto>(dailySchedule);
        }
        public Task UpdateDailyScheduleAsync(UpdateDailyScheduleDto updateDailyScheduleDto)
        {
            var dailySchedule = _mapper.Map<DailySchedule>(updateDailyScheduleDto);
            return _dailyScheduleRepository.UpdateAsync(dailySchedule);
        }
        public async Task DeleteDailyScheduleAsync(Guid dailyScheduleId)
        {
            await _dailyScheduleRepository.DeleteAsync(dailyScheduleId);
        }

        public async Task<DailyScheduleDto?> GetDailySchedulesByClassAndDateAsync(Guid classId, DateOnly date)
        {
            var dailySchedule = await _dailyScheduleRepository.GetByClassAndDataAsync(classId, date)
                ?? throw new NotFoundException($"No daily schedule found for class {classId} on {date:yyyy-MM-dd}");

            return _mapper.Map<DailyScheduleDto>(dailySchedule);
        }

        public async Task<DailyScheduleDto?> GetDailySchedulesByClassTodayAsync(Guid classId)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            
            var dailySchedule = await _dailyScheduleRepository.GetByClassAndDataAsync(classId, today)
                ?? throw new NotFoundException($"No daily schedule found for class {classId} on {today:yyyy-MM-dd}");

            return _mapper.Map<DailyScheduleDto>(dailySchedule);
        }
    }
}
