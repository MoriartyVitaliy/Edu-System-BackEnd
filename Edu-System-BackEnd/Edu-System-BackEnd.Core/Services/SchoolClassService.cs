using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        public SchoolClassService(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }
        public async Task<IEnumerable<SchoolClass>> GetAll()
        {
            return await _schoolClassRepository.GetAllAsync();
        }
        public async Task<SchoolClass?> GetById(Guid id)
        {
            return await _schoolClassRepository.GetByIdAsync(id);
        }
        public async Task Create(SchoolClass schoolClass)
        {
            await _schoolClassRepository.AddAsync(schoolClass);
        }
        public async Task Update(SchoolClass schoolClass)
        {
            await _schoolClassRepository.UpdateAsync(schoolClass);
        }
        public async Task Delete(Guid id)
        {
            await _schoolClassRepository.DeleteAsync(id);
        }
    }
}
