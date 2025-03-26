using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly Edu_System_BackEndDbContext _context;
        public ParentRepository(Edu_System_BackEndDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Parent>> GetAllAsync()
        {
            return await _context.Parents
                .Include(p => p.StudentParents)
                    .ThenInclude(sp => sp.Student)
                .Include(p => p.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }
        public Task<Parent?> GetByIdAsync(Guid id)
        {
            return _context.Parents
                .Include(p => p.StudentParents)
                    .ThenInclude(sp => sp.Student)
                .Include(p => p.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Parent parent)
        {
            var parentRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Parent");
            if (parentRole == null)
            {
                throw new NotFoundException("Role 'Parent' not found via data base");
            }

            parent.UserRoles = new List<UserRole> 
            {
                new UserRole { RoleId = parentRole.Id, UserId = parent.Id } 
            };

            await _context.Parents.AddAsync(parent);
            await _context.SaveChangesAsync();
        }
        public Task UpdateAsync(Parent parent)
        {
            var existingParent = _context.Parents.FirstOrDefault(p => p.Id == parent.Id);
            if (existingParent == null) throw new NotFoundException($"Parent with ID {parent.Id} not found.");

            _context.Parents.Update(parent);
            return _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.Id == id);
            if (parent != null)
            {
                _context.Parents.Remove(parent);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateStudentToParent(Guid parentId, Guid studentId)
        {
            var parentExists = await _context.Parents.AnyAsync(p => p.Id == parentId);
            if (!parentExists) throw new NotFoundException($"Parent with ID {parentId} not found.");

            var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
            if (!studentExists) throw new NotFoundException($"Student with ID {studentId} not found.");

            var studentParent = new StudentParent { ParentId = parentId, StudentId = studentId };
            await _context.StudentParents.AddAsync(studentParent);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Student>> GetParentStudents(Guid parentId)
        {
            return await _context.StudentParents
                .Where(sp => sp.ParentId == parentId)
                .Include(sp => sp.Student)
                    .ThenInclude(s => s.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(sp => sp.Student.SchoolClass)
                .Select(sp => sp.Student)
                .ToListAsync();
        }


    }
}