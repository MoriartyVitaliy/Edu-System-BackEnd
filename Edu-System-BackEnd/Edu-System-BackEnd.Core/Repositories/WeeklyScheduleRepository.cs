using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class WeeklyScheduleRepository : BaseRepository, IWeeklyScheduleRepository
    {
        public WeeklyScheduleRepository(Edu_System_BackEndDbContext context) : base(context) { }

        public async Task<IEnumerable<WeeklySchedule>> GetAllAsync()
        {
            return await _context.WeeklySchedules
                .Include(ws => ws.DailySchedules)
                    .ThenInclude(ds => ds.Lessons)
                        .ThenInclude(l => l.Subject)
                    .ThenInclude(ds => ds.Lessons)
                        .ThenInclude(l => l.Teacher)
                .Include(ws => ws.SchoolClass)
                .ToListAsync();
        }
        public async Task<WeeklySchedule?> GetByIdAsync(Guid id)
        {
            return await _context.WeeklySchedules
                .Include(ws => ws.DailySchedules)
                    .ThenInclude(ds => ds.Lessons)
                        .ThenInclude(l => l.Subject)
                    .ThenInclude(ds => ds.Lessons)
                        .ThenInclude(l => l.Teacher)
                .Include(ws => ws.SchoolClass)
                .FirstOrDefaultAsync(ws => ws.Id == id);
        }
        public async Task AddAsync(WeeklySchedule entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.WeeklySchedules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(WeeklySchedule entity)
        {
            if (await _context.WeeklySchedules.FirstOrDefaultAsync(s => s.Id == entity.Id) == null)
            {
                throw new InvalidOperationException($"Weekly schedule with id {entity.Id} not found.");
            }
            _context.WeeklySchedules.Update(entity);
            await _context.SaveChangesAsync();
        }
        public Task DeleteAsync(Guid id)
        {
            var weeklySchedule = _context.WeeklySchedules.FirstOrDefault(ws => ws.Id == id);
            if (weeklySchedule == null)
                throw new NotFoundException("Weekly schedule not found.");

            _context.WeeklySchedules.Remove(weeklySchedule);
            return _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<WeeklySchedule?>> GetAllWeeklySchedulesByClassId(Guid schoolClassId)
        {
            return await _context.WeeklySchedules
                .Where(ws => ws.SchoolClassId == schoolClassId)
                .Include(ws => ws.DailySchedules)
                    .ThenInclude(ds => ds.Lessons)
                        .ThenInclude(l => l.Subject)
                .Include(ws => ws.DailySchedules)
                    .ThenInclude(ds => ds.Lessons)
                        .ThenInclude(l => l.Teacher)
                .ToListAsync();
        }

        public async Task<WeeklySchedule?> GetCurrentWeekScheduleAsync(Guid schoolClassId, DateTime startOfWeek)
        {
            return await _context.WeeklySchedules
                .Include(ws => ws.DailySchedules)
                .ThenInclude(ds => ds.Lessons)
                .FirstOrDefaultAsync(ws => ws.SchoolClassId == schoolClassId &&
                                           ws.DailySchedules.Any(ds => ds.Date >= DateOnly.FromDateTime(startOfWeek) &&
                                                                       ds.Date <= DateOnly.FromDateTime(startOfWeek.AddDays(6))));
        }
        public async Task<bool> ExistsForWeekAsync(Guid schoolClassId, DateTime startOfWeek)
        {
            return await _context.WeeklySchedules
                .AnyAsync(ws => ws.SchoolClassId == schoolClassId &&
                                ws.DailySchedules.Any(ds => ds.Date >= DateOnly.FromDateTime(startOfWeek) &&
                                                            ds.Date <= DateOnly.FromDateTime(startOfWeek.AddDays(6))));
        }
    }
}
