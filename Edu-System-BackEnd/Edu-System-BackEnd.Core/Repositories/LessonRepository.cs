using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class LessonRepository : BaseRepository, ILessonRepository
    {
        public LessonRepository(Edu_System_BackEndDbContext context) : base(context) { }

        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return await _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .ToListAsync();
        }

        public async Task<Lesson?> GetByIdAsync(Guid id)
        {
            return await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        }        
        public Task AddAsync(Lesson entity)
        {
            var lesson = _context.Lessons.Add(entity);
            return _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Lesson entity)
        {
            _context.Lessons.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null)
            {
                throw new NotFoundException("Lesson not found.");
            }
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public Task<List<Lesson>> GetLessonsByScheduleIdAsync(Guid scheduleId)
        {
            throw new NotImplementedException();
        }
    }
}
