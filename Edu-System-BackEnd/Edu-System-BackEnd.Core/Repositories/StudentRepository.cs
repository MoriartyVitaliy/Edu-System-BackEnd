using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{


    //TODO : Implement methods and add exceptions to them
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(Edu_System_BackEndDbContext context) : base(context) { }
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
        public async Task AddAsync(Student student)
        {
            var studentRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Student");

            if (studentRole == null)
                throw new NotFoundException("Role 'Student' not found via database.");
            

            student.UserRoles = new List<UserRole> { new UserRole { RoleId = studentRole.Id, UserId = student.Id } };
            var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(sc => sc.Id == student.SchoolClassId);
            if (schoolClass == null)
                throw new NotFoundException("School class not found.");

            student.SchoolClass = schoolClass;

            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            if (existingStudent == null)
                throw new NotFoundException($"Student with ID {student.Id} not found.");


            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudentClassAsync(Guid studentId, Guid classId)
        {
            var student = await _context.Students.FindAsync(studentId)
                ?? throw new NotFoundException("Student not found.");

            var schoolClass = await _context.SchoolClasses.FindAsync(classId)
                ?? throw new NotFoundException("School class not found.");

            student.SchoolClass = schoolClass;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id)
                ?? throw new NotFoundException($"Student with ID {id} not found.");
            
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}