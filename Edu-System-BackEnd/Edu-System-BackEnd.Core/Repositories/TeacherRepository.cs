using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    
    public class TeacherRepository : BaseRepository, ITeacherRepository
    {
        public TeacherRepository(Edu_System_BackEndDbContext context) : base(context) { }

        public async Task AddAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher); 
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            
            if (teacher == null)
                throw new NotFoundException("Teacher not found");
            
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var teacher = await _context.Teachers
                .Include(t => t.ClassSupervisions)
                .FirstOrDefaultAsync(t => t.UserId == teacherId);

            if (teacher == null)
                throw new NotFoundException("Teacher not found");

            var schoolClass = teacher.ClassSupervisions.FirstOrDefault(c => c.Id == classId);

            if (schoolClass == null)
                throw new NotFoundException("Class not found");

            teacher.ClassSupervisions.Remove(schoolClass);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(s => s.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(s => s.ClassSupervisions)
                .ToListAsync();
        }

        public async Task<IEnumerable<SchoolClass>> GetAllTeacherClassesAsync(Guid teacherId)
        {
            return await _context.SchoolClasses
                .Where(sc => sc.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _context.Teachers
                .Include(s => s.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(s => s.ClassSupervisions)
                .FirstOrDefaultAsync(t => t.UserId == id);
        }

        public async Task<(Teacher, SchoolClass)> GetTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var teacher = await _context.Teachers
                .Include(t => t.ClassSupervisions)
                .FirstOrDefaultAsync(t => t.UserId == teacherId);

            if (teacher == null)
                throw new NotFoundException("Teacher not found");

            var schoolClass = teacher.ClassSupervisions.FirstOrDefault(c => c.Id == classId);

            if (schoolClass == null)
                throw new NotFoundException("Class not found for this teacher");

            return (teacher, schoolClass);
        }

        public async Task UpdateAsync(Teacher entity)
        {
            _context.Teachers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.UserId == teacherId);

            if (teacher == null)
                throw new NotFoundException("Teacher not found");

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(sc => sc.Id == classId);

            if (schoolClass == null)
                throw new NotFoundException("Class not found");

            schoolClass.TeacherId = teacherId;
            _context.SchoolClasses.Update(schoolClass);
            await _context.SaveChangesAsync();
        }
    }
}
