using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Providers;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class WeeklyScheduleService : IWeeklyScheduleService
    {
        private readonly IWeeklyScheduleRepository _weeklyScheduleRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IScheduleInfoProvider _scheduleInfoProvider;
        private readonly IMapper _mapper;
        public WeeklyScheduleService(IWeeklyScheduleRepository scheduleRepository, IScheduleInfoProvider scheduleInfoProvider, IMapper mapper, ISchoolClassRepository schoolClassRepository)
        {
            _weeklyScheduleRepository = scheduleRepository;
            _scheduleInfoProvider = scheduleInfoProvider;
            _mapper = mapper;
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<IEnumerable<WeeklyScheduleDto>> GetAllWeeklySchedulesAsync()
        {
            var dailySchedules = await _weeklyScheduleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WeeklyScheduleDto>>(dailySchedules);
        }

        public async Task<WeeklyScheduleDto?> GetWeeklyScheduleByIdAsync(Guid dailyScheduleId)
        {
            var dailySchedule = await _weeklyScheduleRepository.GetByIdAsync(dailyScheduleId);
            return _mapper.Map<WeeklyScheduleDto?>(dailySchedule);
        }
        public async Task<WeeklyScheduleDto> CreateWeeklyScheduleAsync(CreateWeeklyScheduleDto createWeeklyScheduleDto)
        {
            var weeklySchedule = _mapper.Map<WeeklySchedule>(createWeeklyScheduleDto);
            weeklySchedule.Id = Guid.NewGuid();

            await _weeklyScheduleRepository.AddAsync(weeklySchedule);
            return _mapper.Map<WeeklyScheduleDto>(weeklySchedule);
        }
        public Task UpdateWeeklyScheduleAsync(UpdateWeeklyScheduleDto updateWeeklyScheduleDto)
        {
            var weeklySchedule = _mapper.Map<WeeklySchedule>(updateWeeklyScheduleDto);
            return _weeklyScheduleRepository.UpdateAsync(weeklySchedule);
        }
        public async Task DeleteWeeklyScheduleAsync(Guid dailyScheduleId)
        {
            await _weeklyScheduleRepository.DeleteAsync(dailyScheduleId);
        }

        public async Task<WeeklyScheduleDto> CreateNextWeekScheduleAsync(Guid schoolClassId)
        {
            var today = DateTime.UtcNow.Date;
            var startOfNextWeek = today.AddDays(-(int)today.DayOfWeek + 8);

            if (await _weeklyScheduleRepository.ExistsForWeekAsync(schoolClassId, startOfNextWeek))
            {
                throw new BadRequestException("Schedule for next week already exist");
            }

            var weeklySchedule = new WeeklySchedule
            {
                Id = Guid.NewGuid(),
                SchoolClassId = schoolClassId
            };

            await _weeklyScheduleRepository.AddAsync(weeklySchedule);

            await _scheduleInfoProvider.CreateDailySchedulesForWeekAsync(weeklySchedule.Id, schoolClassId, startOfNextWeek);

            return _mapper.Map<WeeklyScheduleDto>(weeklySchedule);
        }

        public async Task<WeeklyScheduleDto?> GetCurrentWeekScheduleAsync(Guid schoolClassId)
        {
            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1);
            var weeklySchedule = await _weeklyScheduleRepository.GetCurrentWeekScheduleAsync(schoolClassId, startOfWeek);
            if (weeklySchedule == null)
            {
                throw new NotFoundException("Weekly schedule not found");
            }
            return _mapper.Map<WeeklyScheduleDto?>(weeklySchedule);
        }
        public async Task<IEnumerable<WeeklyScheduleDto>> GetAllWeeklySchedulesByClassId(Guid schoolClassId)
        {
            var weeklySchedule = await _weeklyScheduleRepository.GetAllWeeklySchedulesByClassId(schoolClassId);
            if (weeklySchedule == null)
            {
                throw new NotFoundException("Weekly schedule not found");
            }
            return _mapper.Map<IEnumerable<WeeklyScheduleDto>>(weeklySchedule);
        }

        public async Task<IEnumerable<WeeklyScheduleDto>> GetAllWeeklySchedulesByClassName(string className)
        {
            var schoolClass = await _schoolClassRepository.GetByNameAsync(className);
            if (schoolClass == null)
            {
                throw new NotFoundException($"School class with name '{className}' not found");
            }

            return await GetAllWeeklySchedulesByClassId(schoolClass.Id);
        }
    }
}
