using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface ISchoolClassService
    {
        public Task<IEnumerable<SchoolClassDto>> GetAllSchoolClassAsync();
        public Task<SchoolClassDto> GetSchoolClassByIdAsync(Guid id);
        public Task<SchoolClassDto> CreateSchoolClassAsync(CreateSchoolClassDto schoolClass);
        public Task UpdateSchoolClassAsync(UpdateSchoolClassDto schoolClass);
        public Task DeleteSchoolClassAsync(Guid id);
    }
}
