using Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence
{
    public class Edu_System_BackEndDbContext : DbContext
    {
        public Edu_System_BackEndDbContext(DbContextOptions<Edu_System_BackEndDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<LessonMark> LessonMarks { get; set; }
        public DbSet<HomeworkMark> HomeworkMarks { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        //public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditLogConfiguration).Assembly);

            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<Parent>().ToTable("parents");
            modelBuilder.Entity<Teacher>().ToTable("teachers");

            //User Roles
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            //Student Parents
            modelBuilder.Entity<StudentParent>()
                .HasKey(sp => new { sp.StudentId, sp.ParentId });

            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentParents)
                .HasForeignKey(sp => sp.StudentId);

            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Parent)
                .WithMany(p => p.StudentParents)
                .HasForeignKey(sp => sp.ParentId);

            //School Class
            modelBuilder.Entity<SchoolClass>()
                .HasMany(sc => sc.Students)
                .WithOne(s => s.SchoolClass)
                .HasForeignKey(s => s.SchoolClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SchoolClass>()
                .HasOne(sc => sc.Teacher)
                .WithMany(t => t.ClassSupervisions)
                .HasForeignKey(sc => sc.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            //Lesson
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.SchoolClass)
                .WithMany(sc => sc.Lessons)
                .HasForeignKey(l => l.SchoolClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasOne(s => s.Subject)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(t => t.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherId);

            modelBuilder.Entity<Lesson>()
                .HasMany(a => a.Attendances)
                .WithOne(l => l.Lesson)
                .HasForeignKey(a => a.LessonId);

            modelBuilder.Entity<Lesson>()
                .HasMany(lm => lm.LessonMarks)
                .WithOne(l => l.Lesson)
                .HasForeignKey(lm => lm.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasMany(hm => hm.Homeworks)
                .WithOne(l => l.Lesson)
                .HasForeignKey(hm => hm.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            //Homework

            modelBuilder.Entity<Homework>()
                .HasMany(hs => hs.Submissions)
                .WithOne(h => h.Homework)
                .HasForeignKey(hs => hs.HomeworkId);

            //Homework Submission

            modelBuilder.Entity<HomeworkSubmission>()
                .HasOne(hs => hs.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(hs => hs.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HomeworkSubmission>()
                .HasOne(hs => hs.Homework)
                .WithMany(h => h.Submissions)
                .HasForeignKey(hs => hs.HomeworkId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HomeworkSubmission>()
                .HasOne(hs => hs.HomeworkMark)
                .WithOne(m => m.HomeworkSubmission)
                .HasForeignKey<HomeworkSubmission>(hs => hs.HomeworkMarkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
