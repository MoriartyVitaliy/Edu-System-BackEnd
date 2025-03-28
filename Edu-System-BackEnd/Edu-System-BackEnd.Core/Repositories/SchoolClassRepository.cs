using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly Edu_System_BackEndDbContext _context;
        public SchoolClassRepository(Edu_System_BackEndDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SchoolClass>> GetAllAsync()
        {
            return await _context.SchoolClasses
                .Include(s => s.Students)
                .Include(t => t.Teacher)
                .ToListAsync();
        }
        public async Task<SchoolClass?> GetByIdAsync(Guid id)
        {
            return await _context.SchoolClasses
                .Include(s => s.Students)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task AddAsync(SchoolClass schoolClass)
        {
            await _context.SchoolClasses.AddAsync(schoolClass);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(SchoolClass schoolClass)
        {
            var schoolClassExists = await _context.SchoolClasses.AnyAsync(s => s.Id == schoolClass.Id);
            if (!schoolClassExists) throw new NotFoundException($"SchoolClass with id {schoolClass.Id} not found.");

            _context.SchoolClasses.Update(schoolClass);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(s => s.Id == id);
            if (schoolClass == null)
            {
                throw new NotFoundException($"SchoolClass with id {id} not found.");
            }
            _context.SchoolClasses.Remove(schoolClass);
            await _context.SaveChangesAsync();
        }
    }
}
