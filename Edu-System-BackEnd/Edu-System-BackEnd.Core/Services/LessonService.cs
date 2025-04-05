using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LessonDto>>(lessons);
        }

        public async Task<LessonDto?> GetLessonByIdAsync(Guid lessonId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId)
                ?? throw new NotFoundException($"Lesson with id {lessonId} not found.");

            return _mapper.Map<LessonDto>(lesson);
        }
        public async Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(createLessonDto);
            lesson.Id = Guid.NewGuid();

            await _lessonRepository.AddAsync(lesson);
            return _mapper.Map<LessonDto>(lesson);
        }
        public async Task UpdateLessonAsync(UpdateLessonDto updateLessonDto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(updateLessonDto.Id)
                ?? throw new NotFoundException($"Lesson with id {updateLessonDto.Id} not found.");

            _mapper.Map(updateLessonDto, lesson);
            await _lessonRepository.UpdateAsync(lesson);
        }
        public async Task DeleteLessonAsync(Guid lessonId)
        {
            await _lessonRepository.DeleteAsync(lessonId);
        }

        public Task<Dictionary<DayOfWeek, List<LessonDto>>> GetWeeklyScheduleAsync(Guid schoolClassId)
        {
            throw new NotImplementedException();
        }
    }
}
