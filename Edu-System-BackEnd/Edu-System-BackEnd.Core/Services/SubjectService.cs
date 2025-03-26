using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _subjectRepository.GetAllAsync();
        }
        public async Task<Subject?> GetSubjectByIdAsync(Guid id)
        {
            return await _subjectRepository.GetByIdAsync(id);
        }
        public async Task CreateSubjectAsync(Subject subject)
        {
            await _subjectRepository.AddAsync(subject);
        }
        public async Task UpdateSubjectAsync(Subject subjectDto)
        {
            await _subjectRepository.UpdateAsync(subjectDto);
        }
        public async Task DeleteSubjectAsync(Guid id)
        {
            await _subjectRepository.DeleteAsync(id);
        }
    }
}
