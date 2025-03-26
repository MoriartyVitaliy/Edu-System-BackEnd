using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly Edu_System_BackEndDbContext _context;
        public TeacherRepository(Edu_System_BackEndDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(s => s.ClassSupervisions)
                .Include(s => s.UserRoles)
                    .ThenInclude(r => r.Role)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _context.Teachers
                .Include(s => s.ClassSupervisions)
                .Include(s => s.UserRoles)
                    .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Teacher teacher)
        {
            var teacherRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Teacher");

            if (teacherRole == null)
            {
                throw new Exception("Роль 'Teacher' не знайдена в базі даних.");
            }

            teacher.UserRoles = new List<UserRole> { new UserRole { RoleId = teacherRole.Id, UserId = teacher.Id } };

            await _context.Teachers.AddAsync(teacher);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var teacher = await _context.Teachers
                .Include(t => t.ClassSupervisions) 
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
            {
                throw new Exception("Teacher not found.");
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (schoolClass == null)
            {
                throw new Exception("School class not found.");
            }

            // Переконуємося, що клас ще не додано
            if (!teacher.ClassSupervisions.Any(c => c.Id == classId))
            {
                teacher.ClassSupervisions.Add(schoolClass);
            }

            await _context.SaveChangesAsync();
        }

    }
}
