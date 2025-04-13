using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence
{
    public class DataSeed
    {
        public static void SeedData(Edu_System_BackEndDbContext context)
        {
            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();

                var schoolClass = new SchoolClass { Name = "11-A" };

                var studentRole = new Role { Name = "Student" };
                var teacherRole = new Role { Name = "Teacher" };
                var parentRole = new Role { Name = "Parent" };

                var mathSubject = new Subject { Name = "Математика" };
                var historySubject = new Subject { Name = "Історія" };

                var studentUser = new User
                {
                    FirstName = "Іван",
                    MiddleName = "Петрович",
                    LastName = "Петров",
                    Email = "ivan@example.com",
                    PhoneNumber = "+380123456789",
                    UserRoles = new List<UserRole> { new UserRole { Role = studentRole } }
                };
                studentUser.PasswordHash = passwordHasher.HashPassword(studentUser, "Qwerty123!");

                var teacherUser = new User
                {
                    FirstName = "Марія",
                    MiddleName = "Василівна",
                    LastName = "Сидоренко",
                    Email = "maria@example.com",
                    PhoneNumber = "+380987654321",
                    UserRoles = new List<UserRole> { new UserRole { Role = teacherRole } }
                };
                teacherUser.PasswordHash = passwordHasher.HashPassword(teacherUser, "Qwerty123!");

                var parentUser = new User
                {
                    FirstName = "Олег",
                    MiddleName = "Сергійович",
                    LastName = "Симака",
                    Email = "oleg@example.com",
                    PhoneNumber = "+380998877665",
                    UserRoles = new List<UserRole> { new UserRole { Role = parentRole } }
                };
                parentUser.PasswordHash = passwordHasher.HashPassword(parentUser, "Qwerty123!");

                var student = new Student
                {
                    User = studentUser,
                    SchoolClass = schoolClass
                };

                var teacher = new Teacher
                {
                    User = teacherUser,
                    ClassSupervisions = new List<SchoolClass> { schoolClass }
                };

                var parent = new Parent
                {
                    User = parentUser,
                    StudentParents = new List<StudentParent>
                    {
                        new StudentParent { Student = student }
                    }
                };

                context.Roles.AddRange(studentRole, teacherRole, parentRole);
                context.SchoolClasses.Add(schoolClass);
                context.Subjects.AddRange(mathSubject, historySubject);
                context.Users.AddRange(studentUser, teacherUser, parentUser);
                context.Students.Add(student);
                context.Teachers.Add(teacher);
                context.Parents.Add(parent);

                context.SaveChanges();
            }
        }
    }
}
