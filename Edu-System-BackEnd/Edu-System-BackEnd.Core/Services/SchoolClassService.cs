using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IMapper _mapper;
        public SchoolClassService(ISchoolClassRepository schoolClassRepository, IMapper mapper)
        {
            _schoolClassRepository = schoolClassRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SchoolClassDto>> GetAllSchoolClassAsync()
        {
            var schoolClasses = await _schoolClassRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SchoolClassDto>>(schoolClasses);
        }
        public async Task<SchoolClassDto> GetSchoolClassByIdAsync(Guid id)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"School class with id {id} not found");
            return _mapper.Map<SchoolClassDto>(schoolClass);
        }
        public async Task<SchoolClassDto> CreateSchoolClassAsync(CreateSchoolClassDto createSchoolClassDto)
        {
            var schoolClass = _mapper.Map<SchoolClass>(createSchoolClassDto);
            schoolClass.Id = Guid.NewGuid();

            await _schoolClassRepository.AddAsync(schoolClass);
            return _mapper.Map<SchoolClassDto>(schoolClass);
        }
        public async Task UpdateSchoolClassAsync(UpdateSchoolClassDto updateSchoolClassDto)
        {
            var schoolClass = _mapper.Map<SchoolClass>(updateSchoolClassDto);
            await _schoolClassRepository.UpdateAsync(schoolClass);
        }
        public async Task DeleteSchoolClassAsync(Guid id)
        {
            await _schoolClassRepository.DeleteAsync(id);
        }
    }
}
