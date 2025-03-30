using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Subject;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }
        public async Task<SubjectDto?> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Subject with id {id} not found");

            return _mapper.Map<SubjectDto>(subject);
        }
        public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto createSubjectDto)
        {
            var subject = _mapper.Map<Subject>(createSubjectDto);
            subject.Id = Guid.NewGuid();

            await _subjectRepository.AddAsync(subject);
            return _mapper.Map<SubjectDto>(subject);
        }
        public async Task UpdateSubjectAsync(UpdateSubjectDto updateSubjectDto)
        {
            var subject = await _subjectRepository.GetByIdAsync(updateSubjectDto.Id);
            if (subject == null)
                throw new NotFoundException($"Subject with ID {updateSubjectDto.Id} not found.");
            
            _mapper.Map(updateSubjectDto, subject);
            await _subjectRepository.UpdateAsync(subject);

        }
        public async Task DeleteSubjectAsync(Guid id)
        {
            await _subjectRepository.DeleteAsync(id);
        }
    }
}
