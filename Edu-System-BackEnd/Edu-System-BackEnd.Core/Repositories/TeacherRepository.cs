using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
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
                throw new NotFoundException("Роль 'Teacher' не знайдена в базі даних.");
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
        public async Task<(Teacher, SchoolClass)> GetTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId)
                ?? throw new NotFoundException("Teacher not found.");
            var schoolClass = await _context.SchoolClasses.FindAsync(classId)
                ?? throw new NotFoundException("School class not found.");
            return (teacher, schoolClass);
        }
        public async Task UpdateTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var (teacher, schoolClass) = await GetTeacherClassAsync(teacherId, classId);

            schoolClass.Teacher = teacher;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeacherClassAsync(Guid teacherId, Guid classId)
        {
            var (teacher, schoolClass) = await GetTeacherClassAsync(teacherId, classId);

            teacher.ClassSupervisions.Remove(schoolClass);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SchoolClass>> GetAllTeacherClassesAsync(Guid teacherId)
        {
            var classes = await _context.SchoolClasses
                .Where(sc => sc.TeacherId == teacherId)
                .ToListAsync();

            return classes;
        }
    }
}
