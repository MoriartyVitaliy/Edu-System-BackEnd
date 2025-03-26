using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Services
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly IMapper _mapper;
        public ParentService(IParentRepository parentRepository, IMapper mapper)
        {
            _parentRepository = parentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ParentDto>> GetAllParentsAsync()
        {
            var parents = await _parentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParentDto>>(parents);
        }
        public async Task<ParentDto?> GetParentByIdAsync(Guid id)
        {
            var parent = await _parentRepository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"Parent with ID {id} not found.");
            return _mapper.Map<ParentDto>(parent);
        }
        public async Task<ParentDto> CreateParentAsync(CreateParentDto createParentDto)
        {
            var parent = _mapper.Map<Parent>(createParentDto);
            parent.Id = Guid.NewGuid();

            await _parentRepository.AddAsync(parent);
            return _mapper.Map<ParentDto>(parent);
        }
        public async Task UpdateParentAsync(UpdateParentDto updateParentDto)
        {
            var parent = _mapper.Map<Parent>(updateParentDto);
            await _parentRepository.UpdateAsync(parent);
        }
        public async Task DeleteParentAsync(Guid id)
        {
            var parent = await _parentRepository.GetByIdAsync(id) ?? throw new NotFoundException("Parent not found.");
            await _parentRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<StudentDto>> GetParentStudentAsync(Guid parentId)
        {
            var students = await _parentRepository.GetParentStudents(parentId);

            if(!students.Any()) throw new NotFoundException("No students found for parent.");

            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
        public async Task UpdateParentStudentAsync(Guid parentId, Guid studentId)
        {
            await _parentRepository.UpdateStudentToParent(parentId, studentId);
        }
    }
}
