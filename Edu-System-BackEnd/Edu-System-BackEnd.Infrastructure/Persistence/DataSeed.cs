using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence
{
    public class DataSeed
    {
        public static void SeedData(Edu_System_BackEndDbContext context)
        {
            if (!context.Users.Any())
            {
                var schoolClass = new SchoolClass { Name = "11-А" };

                var studentRole = new Role { Name = "Student" };
                var teacherRole = new Role { Name = "Teacher" };
                var parentRole = new Role { Name = "Parent" };

                var mathSubject = new Subject { Name = "Математика" };
                var histortSubject = new Subject { Name = "Історія" };

                var studentUser = new Student
                {
                    FirstName = "Іван",
                    MiddleName = "Петрович",
                    LastName = "Петров",
                    Email = "ivan@example.com",
                    PasswordHash = "123456",
                    UserRoles = new List<UserRole> { new UserRole { Role = studentRole } },
                    SchoolClassId = schoolClass.Id,
                    SchoolClass = schoolClass
                };

                var teacherUser = new Teacher
                {
                    FirstName = "Марія",
                    MiddleName = "Василівна",
                    LastName = "Сидоренко",
                    Email = "maria@example.com",
                    PasswordHash = "123456",
                    ClassSupervisions = new List<SchoolClass> { schoolClass },
                    UserRoles = new List<UserRole> { new UserRole { Role = teacherRole } }
                };

                var parentUser = new Parent
                {
                    FirstName = "Олег",
                    MiddleName = "Сергійович",
                    LastName = "Симака",
                    Email = "oleg@example.com",
                    PasswordHash = "123456",
                    UserRoles = new List<UserRole> { new UserRole { Role = parentRole } },
                    StudentParents = new List<StudentParent> { new StudentParent { Student = studentUser } }
                };

                context.Roles.AddRange(studentRole, teacherRole, parentRole);
                context.Students.Add(studentUser);
                context.Teachers.Add(teacherUser);
                context.Parents.Add(parentUser);
                context.SchoolClasses.Add(schoolClass);
                context.Subjects.AddRange(mathSubject, histortSubject);

                context.SaveChanges();
            }
        }
    }

}
