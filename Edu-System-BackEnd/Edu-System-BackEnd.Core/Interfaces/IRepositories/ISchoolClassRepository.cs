using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories
{
    public interface ISchoolClassRepository : ICrudRepository<SchoolClass>
    {
        Task<SchoolClass?> GetByNameAsync(string name);
    }
}
