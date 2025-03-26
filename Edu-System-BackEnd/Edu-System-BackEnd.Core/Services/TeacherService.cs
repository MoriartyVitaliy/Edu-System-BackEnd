using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllAsync();
        }
        public async Task<Teacher?> GetTeacherByIdAsync(Guid id)
        {
            return await _teacherRepository.GetByIdAsync(id);
        }
        public async Task CreateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.AddAsync(teacher);
        }
        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.UpdateAsync(teacher);
        }
        public async Task UpdateTeacherClassAsync(Guid teacherId, Guid classId)
        {
            await _teacherRepository.UpdateTeacherClassAsync(teacherId, classId);
        }
        public async Task DeleteTeacherAsync(Guid id)
        {
            await _teacherRepository.DeleteAsync(id);
        }
    }
}
