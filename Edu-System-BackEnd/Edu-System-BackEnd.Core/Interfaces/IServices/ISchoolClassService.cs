using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface ISchoolClassService
    {
        public Task<IEnumerable<SchoolClass>> GetAll();
        public Task<SchoolClass> GetById(Guid id);
        public Task Create(SchoolClass schoolClass);
        public Task Update(SchoolClass schoolClass);
        public Task Delete(Guid id);
    }
}
