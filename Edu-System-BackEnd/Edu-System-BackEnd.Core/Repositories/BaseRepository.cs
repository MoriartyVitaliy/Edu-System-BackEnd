using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly Edu_System_BackEndDbContext _context;
     
        protected BaseRepository(Edu_System_BackEndDbContext context)
        {
            _context = context;
        }
    }
}
