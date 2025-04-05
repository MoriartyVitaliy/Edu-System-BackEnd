using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetAllLessonsAsync();
        Task<LessonDto?> GetLessonByIdAsync(Guid lessonId);
        Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto);
        Task UpdateLessonAsync(UpdateLessonDto updateLessonDto);
        Task DeleteLessonAsync(Guid lessonId);
        Task<Dictionary<DayOfWeek, List<LessonDto>>> GetWeeklyScheduleAsync(Guid schoolClassId);
    }
}
