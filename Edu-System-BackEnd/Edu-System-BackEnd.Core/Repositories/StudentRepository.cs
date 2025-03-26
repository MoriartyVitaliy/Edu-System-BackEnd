using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{


    //TODO : Implement methods and add exceptions to them
    public class StudentRepository : IStudentRepository
    {
        private readonly Edu_System_BackEndDbContext _context;
        public StudentRepository(Edu_System_BackEndDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.SchoolClass)
                .Include(s => s.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }
        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students
                .Include(s => s.SchoolClass)
                .Include(s => s.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task AddAsync(Student student) //TODO: make return status code
        {
            var studentRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Student");

            if (studentRole == null)
            {
                throw new Exception("Роль 'Student' не знайдена в базі даних.");
            }

            student.UserRoles = new List<UserRole> { new UserRole { RoleId = studentRole.Id, UserId = student.Id } };
            var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(sc => sc.Id == student.SchoolClassId);
            if (schoolClass == null)
            {
                throw new Exception("School class not found.");
            }
            student.SchoolClass = schoolClass;

            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudentClassAsync(Guid studentId, Guid classId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                throw new Exception("Student not found.");
            }
            var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(sc => sc.Id == classId);
            if (schoolClass == null)
            {
                throw new Exception("School class not found.");
            }
            student.SchoolClass = schoolClass;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}