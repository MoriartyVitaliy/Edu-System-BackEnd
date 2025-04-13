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
        public DbSet<DailySchedule> DailySchedules { get; set; }
        public DbSet<WeeklySchedule> WeeklySchedules { get; set; }
        //public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditLogConfiguration).Assembly);

            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<Parent>().ToTable("parents");
            modelBuilder.Entity<Teacher>().ToTable("teachers");


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.MiddleName).HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PhoneNumber).HasMaxLength(20);
            });


            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(t => t.UserId);

                entity.HasOne(t => t.User)
                      .WithOne()
                      .HasForeignKey<Teacher>(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.Lessons)
                      .WithOne(l => l.Teacher)
                      .HasForeignKey(l => l.TeacherId);

                entity.HasMany(t => t.ClassSupervisions)
                      .WithOne(c => c.Teacher)
                      .HasForeignKey(c => c.TeacherId)
                      .OnDelete(DeleteBehavior.SetNull); // если нужно сохранять класс без куратора
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.UserId);

                entity.HasOne(s => s.User)
                      .WithOne()
                      .HasForeignKey<Student>(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.SchoolClass)
                      .WithMany(c => c.Students)
                      .HasForeignKey(s => s.SchoolClassId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.LessonMarks)
                      .WithOne(m => m.Student)
                      .HasForeignKey(m => m.StudentId);

                entity.HasMany(s => s.HomeworkMarks)
                      .WithOne(m => m.Student)
                      .HasForeignKey(m => m.StudentId);

                entity.HasMany(s => s.Attendances)
                      .WithOne(a => a.Student)
                      .HasForeignKey(a => a.StudentId);

                entity.HasMany(s => s.HomeworkSubmissions)
                      .WithOne(h => h.Student)
                      .HasForeignKey(h => h.StudentId);
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.HasKey(p => p.UserId);

                entity.HasOne(p => p.User)
                      .WithOne()
                      .HasForeignKey<Parent>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });




            //User Roles

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId);
            });


            modelBuilder.Entity<StudentParent>(entity =>
            {
                entity.HasKey(sp => new { sp.StudentId, sp.ParentId });

                entity.HasOne(sp => sp.Student)
                      .WithMany(s => s.StudentParents)
                      .HasForeignKey(sp => sp.StudentId);

                entity.HasOne(sp => sp.Parent)
                      .WithMany(p => p.StudentParents)
                      .HasForeignKey(sp => sp.ParentId);
            });

            //Student Parents


            //School Class

            modelBuilder.Entity<SchoolClass>(entity =>
            {
                entity.HasKey(sc => sc.Id);

                entity.HasMany(sc => sc.Students)
                      .WithOne(s => s.SchoolClass)
                      .HasForeignKey(s => s.SchoolClassId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sc => sc.Teacher)
                      .WithMany(t => t.ClassSupervisions)
                      .HasForeignKey(sc => sc.TeacherId)
                      .OnDelete(DeleteBehavior.SetNull);
            });



            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.HasOne(l => l.SchoolClass)
                      .WithMany(sc => sc.Lessons)
                      .HasForeignKey(l => l.SchoolClassId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Subject)
                      .WithMany(s => s.Lessons)
                      .HasForeignKey(l => l.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Teacher)
                      .WithMany(t => t.Lessons)
                      .HasForeignKey(l => l.TeacherId);

                entity.HasMany(a => a.Attendances)
                      .WithOne(l => l.Lesson)
                      .HasForeignKey(a => a.LessonId);

                entity.HasMany(lm => lm.LessonMarks)
                      .WithOne(l => l.Lesson)
                      .HasForeignKey(lm => lm.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(hm => hm.Homeworks)
                      .WithOne(l => l.Lesson)
                      .HasForeignKey(hm => hm.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ds => ds.DailySchedule)
                      .WithMany(l => l.Lessons)
                      .HasForeignKey(ds => ds.DailyScheduleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //Lesson


            //Homework

            modelBuilder.Entity<Homework>(entity =>
            { 
                entity.HasMany(hs => hs.Submissions)
                      .WithOne(h => h.Homework)
                      .HasForeignKey(hs => hs.HomeworkId);
            });

            //Homework Submission

            modelBuilder.Entity<HomeworkSubmission>(entity =>
            {
                entity.HasKey(hs => hs.Id);

                entity.HasOne(hs => hs.Student)
                      .WithMany(s => s.HomeworkSubmissions)
                      .HasForeignKey(hs => hs.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(hs => hs.Homework)
                      .WithMany(h => h.Submissions)
                      .HasForeignKey(hs => hs.HomeworkId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(hs => hs.HomeworkMark)
                      .WithOne(m => m.HomeworkSubmission)
                      .HasForeignKey<HomeworkSubmission>(hs => hs.HomeworkMarkId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //DailySchedule

            modelBuilder.Entity<DailySchedule>(entity =>
            {
                entity.HasKey(ds => ds.Id);

                entity.HasOne(sc => sc.SchoolClass)
                      .WithMany(s => s.Schedules)
                      .HasForeignKey(sc => sc.SchoolClassId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.SchoolClass)
                      .WithMany(sc => sc.Schedules)
                      .HasForeignKey(s => s.SchoolClassId)
                      .OnDelete(DeleteBehavior.Cascade);
            });




            //WeeklySchedule
            modelBuilder.Entity<WeeklySchedule>(entity =>
            {
                entity.HasMany(ws => ws.DailySchedules)
                      .WithOne(w => w.WeeklySchedule)
                      .HasForeignKey(ws => ws.WeeklyScheduleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
