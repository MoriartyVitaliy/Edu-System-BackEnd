using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(Edu_System_BackEndDbContext context) : base(context) { }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddParentAsync(Guid studentId, Guid parentId)
        {
            var student = await _context.Students
                .Include(s => s.StudentParents)
                .FirstOrDefaultAsync(s => s.UserId == studentId);

            if (student == null)
                throw new NotFoundException("Student not found");

            var parent = await _context.Parents.FindAsync(parentId);
            if (parent == null)
                throw new NotFoundException("Parent not found");

            var alreadyLinked = student.StudentParents
                .Any(sp => sp.ParentId == parentId);

            if (alreadyLinked)
                return;

            var relation = new StudentParent
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                ParentId = parentId
            };

            await _context.StudentParents.AddAsync(relation);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(p => p.UserId == id);
            if (student == null)
                throw new NotFoundException("Student not found");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(s => s.SchoolClass)
                .Include(s => s.StudentParents)
                    .ThenInclude(sp => sp.Parent)
                .ToListAsync();
        }


        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students
                .Include(s => s.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(s => s.SchoolClass)
                .Include(s => s.StudentParents)
                    .ThenInclude(sp => sp.Parent)
                .FirstOrDefaultAsync(s => s.UserId == id);
        }

        public Task UpdateAsync(Student entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateClassAsync(Guid studentId, Guid newClassId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
                throw new NotFoundException("Student not found");

            var schoolClass = await _context.SchoolClasses.FindAsync(newClassId);
            if (schoolClass == null)
                throw new NotFoundException("Class not found");

            student.SchoolClassId = newClassId;
            await _context.SaveChangesAsync();
        }

    }
}