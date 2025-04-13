using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public class ParentRepository : BaseRepository, IParentRepository
    {
        public ParentRepository(Edu_System_BackEndDbContext context) : base(context) { }

        public async Task AddAsync(Parent parent)           
        {
            await _context.Users.AddAsync(parent.User);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.UserId == id);
            if (parent == null)
                throw new NotFoundException($"Parent with ID {id} not found.");

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Parent>> GetAllAsync()
        {
            return await _context.Parents
                .Include(p => p.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(p => p.StudentParents)
                    .ThenInclude(sp => sp.Student)
                .ToListAsync();
        }

        public async Task<Parent?> GetByIdAsync(Guid id)
        {
            return await _context.Parents
                .Include(p => p.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                .Include(p => p.StudentParents)
                    .ThenInclude(sp => sp.Student)
                .FirstOrDefaultAsync(p => p.User.Id == id);
        }

        public async Task<IEnumerable<Student>> GetChildrenAsync(Guid parentId)
        {
            var parent = await _context.Parents
                .Include(p => p.StudentParents)
                .ThenInclude(sp => sp.Student)
                .FirstOrDefaultAsync(p => p.User.Id == parentId);

            if (parent == null) throw new NotFoundException("Parent not found");

            return parent.StudentParents.Select(sp => sp.Student);
        }

        public async Task LinkChildAsync(Guid parentId, Guid studentId)
        {
            var exists = await _context.StudentParents
                .AnyAsync(sp => sp.ParentId == parentId && sp.StudentId == studentId);

            if (exists)
                throw new AlreadyExistsException("Link already exists between parent and student");

            var studentParent = new StudentParent
            {
                ParentId = parentId,
                StudentId = studentId
            };

            await _context.StudentParents.AddAsync(studentParent);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Parent entity)
        {
            throw new NotImplementedException();
        }
    }
}