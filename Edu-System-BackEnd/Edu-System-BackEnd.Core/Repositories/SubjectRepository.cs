using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly Edu_System_BackEndDbContext _context;
        public SubjectRepository(Edu_System_BackEndDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                throw new Exception("Subject not found.");
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}
