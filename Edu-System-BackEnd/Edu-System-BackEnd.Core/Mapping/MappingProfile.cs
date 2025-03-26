using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Subject;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.SchoolClassName, opt => opt.MapFrom(src => src.SchoolClass.Name))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()))
                .ReverseMap();
            CreateMap<CreateStudentDto, Student>()
                .ReverseMap();
            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.SchoolClass, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()))
                .ForMember(dest => dest.ClassSupervisions, opt => opt.MapFrom(src => src.ClassSupervisions.Select(c => c.Name).ToList()))
                .ReverseMap();
            CreateMap<CreateTeacherDto, Teacher>()
                .ReverseMap();
            CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(dest => dest.ClassSupervisions, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Subject, SubjectDto>()
                .ReverseMap();
            CreateMap<CreateSubjectDto, Subject>()
                .ReverseMap();
            CreateMap<UpdateSubjectDto, Subject>()
                .ReverseMap();

            CreateMap<SchoolClass, SchoolClassDto>()
                .ForMember(dest => dest.Students, opt =>
                    opt.MapFrom(src => src.Students.Select(src => string.Join(" ", new[] { src.LastName, src.FirstName, src.MiddleName }.Where(x => !string.IsNullOrEmpty(x))))))
                    .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher != null ? $"{src.Teacher.LastName} {src.Teacher.FirstName} {src.Teacher.MiddleName}" : null))
            .ReverseMap();
            CreateMap<CreateSchoolClassDto, SchoolClass>()
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ForMember(dest => dest.Students, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateSchoolClassDto, SchoolClass>()
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Parent, ParentDto>()
                .ForMember(dest => dest.ChildrenNames,
                    opt => opt.MapFrom(src =>
                        src.StudentParents != null
                            ? src.StudentParents
                                .Where(s => s.Student != null)
                                .Select(s => $"{s.Student.LastName} {s.Student.FirstName} {s.Student.MiddleName ?? ""}".Trim())
                                .ToList()
                            : new List<string>()))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(r => r.Role.Name).ToList()));
            CreateMap<CreateParentDto, Parent>()
                .ForMember(dest => dest.StudentParents, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateParentDto, Parent>()
                .ForMember(dest => dest.StudentParents, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
