using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class DailyScheduleRepository : BaseRepository, IDailyScheduleRepository
    {
        public DailyScheduleRepository(Edu_System_BackEndDbContext context) : base(context) { }

        public async Task<IEnumerable<DailySchedule>> GetAllAsync()
        {
            return await _context.DailySchedules
                .AsNoTracking()
                .Include(ds => ds.SchoolClass)
                .Include(ds => ds.Lessons)
                    .ThenInclude(l => l.Subject)
                .Include(ds => ds.Lessons)
                    .ThenInclude(l => l.Teacher)
                .ToListAsync();
        }
        public Task<DailySchedule?> GetByIdAsync(Guid id)
        {
            return _context.DailySchedules
                .AsNoTracking()
                .Include(ds => ds.SchoolClass)
                .Include(ds => ds.Lessons)
                    .ThenInclude(l => l.Subject)
                .Include(ds => ds.Lessons)
                    .ThenInclude(l => l.Teacher)
                .FirstOrDefaultAsync(ds => ds.Id == id);
        }
        public async Task AddAsync(DailySchedule entity)
        {
            var existingSchedule = await _context.DailySchedules
                .FirstOrDefaultAsync(ds => ds.SchoolClassId == entity.SchoolClassId && ds.Date == entity.Date);
            
            if (existingSchedule != null)
                throw new ConflictException($"Daily schedule for class {entity.SchoolClassId} on {entity.Date:yyyy-MM-dd} already exists.");
            
            await _context.DailySchedules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(DailySchedule entity)
        {
            _context.DailySchedules.Update(entity);
            await _context.SaveChangesAsync();
        }
        public Task DeleteAsync(Guid id)
        {
            var dailySchedule = _context.DailySchedules.FirstOrDefault(ds => ds.Id == id);
            if (dailySchedule == null)
                throw new NotFoundException($"Daily schedule with ID {id} not found.");
            _context.DailySchedules.Remove(dailySchedule);
            return _context.SaveChangesAsync();
        }

        public Task<DailySchedule?> GetByClassAndDataAsync(Guid classId, DateOnly date)
        {
            return _context.DailySchedules
                .Include(ds => ds.SchoolClass)
                .Include(ds => ds.Lessons)
                    .ThenInclude(l => l.Subject)
                .Include(ds => ds.Lessons)
                    .ThenInclude(l => l.Teacher)
                .FirstOrDefaultAsync(ds => ds.SchoolClassId == classId && ds.Date == date);
        }

        public async Task AddRangeAsync(IEnumerable<DailySchedule> dailySchedules)
        {
            await _context.DailySchedules.AddRangeAsync(dailySchedules);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Guid weeklyScheduleId, DayOfWeek dayOfWeek)
        {
            return await _context.DailySchedules
                .AnyAsync(ds => ds.WeeklyScheduleId == weeklyScheduleId && ds.Date.DayOfWeek == dayOfWeek);
        }
    }
}
