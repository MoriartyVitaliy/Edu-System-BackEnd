using AutoMapper;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Homework;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Parent;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Role;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Schedule;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Subject;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Enums;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()));
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.SchoolClassName, opt => opt.MapFrom(src => src.SchoolClass.Name))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForPath(dest => dest.User.Roles, opt => opt.MapFrom(src => src.User.UserRoles.Select(ur => ur.Role.Name).ToList()))
                .ReverseMap();

            // CreateStudentDto → Student
            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.SchoolClassId, opt => opt.MapFrom(src => src.SchoolClassId));

            // UpdateStudentDto → Student
            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.SchoolClassId, opt => opt.Ignore()) // Залежно від логіки
                .ForMember(dest => dest.SchoolClass, opt => opt.Ignore())
                .ForMember(dest => dest.StudentParents, opt => opt.Ignore());

            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForPath(dest => dest.User.Roles, opt => opt.MapFrom(src => src.User.UserRoles.Select(ur => ur.Role.Name).ToList()))
                .ForMember(dest => dest.ClassSupervisions, opt => opt.MapFrom(src => src.ClassSupervisions.Select(c => c.Name).ToList()))
                .ReverseMap(); // Реверсний мапінг

            CreateMap<CreateTeacherDto, Teacher>()
                .ReverseMap();
            CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(dest => dest.ClassSupervisions, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Subject, SubjectDto>()
                .ReverseMap();

            CreateMap<CreateSubjectDto, Subject>()
                .ForMember(dest => dest.Lessons, opt => opt.Ignore());
            CreateMap<UpdateSubjectDto, Subject>();


            CreateMap<SchoolClass, SchoolClassDto>()
                .ForMember(dest => dest.Students, opt =>
                    opt.MapFrom(src => src.Students
                        .Where(s => s.User != null)
                        .Select(s =>
                            string.Join(" ", new[] { s.User.LastName, s.User.FirstName, s.User.MiddleName }.Where(x => !string.IsNullOrEmpty(x)))
                        ).ToList()))

                .ForMember(dest => dest.Teacher, opt =>
                    opt.MapFrom(src => src.Teacher != null
                        ? $"{src.Teacher.User.LastName} {src.Teacher.User.FirstName} {src.Teacher.User.MiddleName}"
                        : null))
                .ReverseMap();
            CreateMap<CreateSchoolClassDto, SchoolClass>()
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ForMember(dest => dest.Students, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateSchoolClassDto, SchoolClass>()
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Parent, ParentDto>()
            .ForMember(dest => dest.ChildrenNames, opt => opt.MapFrom(src =>
                 src.StudentParents != null
                     ? src.StudentParents
                         .Where(s => s.Student != null && s.Student.User != null)
                         .Select(s => $"{s.Student.User.LastName} {s.Student.User.FirstName} {s.Student.User.MiddleName ?? ""}".Trim())
                         .ToList()
                     : new List<string>()))
             .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
             .ForPath(dest => dest.User.Roles, opt => opt.MapFrom(src => src.User.UserRoles.Select(ur => ur.Role.Name).ToList()));

            CreateMap<CreateParentDto, Parent>()
                .ForMember(dest => dest.StudentParents, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateParentDto, Parent>()
                .ForMember(dest => dest.StudentParents, opt => opt.Ignore())
                .ReverseMap();


            // ToDo: make mapping #########################


            CreateMap<DailySchedule, DailyScheduleDto>()
                .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));
            CreateMap<CreateDailyScheduleDto, DailySchedule>();
            CreateMap<UpdateDailyScheduleDto, DailySchedule>();



            CreateMap<WeeklySchedule, WeeklyScheduleDto>()
                .ForMember(dest => dest.SchoolClassName, opt => opt.MapFrom(src => src.SchoolClass.Name))
                .ForMember(dest => dest.DailySchedules, opt => opt.MapFrom(src => src.DailySchedules));
            CreateMap<CreateWeeklyScheduleDto, WeeklySchedule>();
            CreateMap<UpdateWeeklyScheduleDto, WeeklySchedule>();

            //##############################################

            CreateMap<Lesson, LessonDto>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.TeacherName, opt =>
                    opt.MapFrom(src => src.Teacher != null
                        ? $"{src.Teacher.User.LastName} {src.Teacher.User.FirstName} {src.Teacher.User.MiddleName}"
                        : null))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.GetName(typeof(LessonType), src.Type)));

            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>()
                .ForAllMembers(opts => opts.NullSubstitute(null));

            CreateMap<Role, RoleDto>()
                .ReverseMap();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            CreateMap<User, UserWithRolesDto>()
                .ForMember(dest => dest.Roles, opt =>
                    opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));
        }
    }
}
