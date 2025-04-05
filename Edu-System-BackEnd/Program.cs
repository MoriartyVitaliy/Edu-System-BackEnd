
using Edu_System_BackEnd.Edu_System_BackEnd.API.Middleware;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Mapping;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Providers;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Repositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Services;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.External;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace Edu_System_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            InitializeDatabase(app);

            ConfigurePipeline(app);

            app.Run();            
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<Edu_System_BackEndDbContext>(provider =>
            {
                var options = new DbContextOptionsBuilder<Edu_System_BackEndDbContext>()
                    .UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                    .UseSnakeCaseNamingConvention()
                    .Options;
                return new Edu_System_BackEndDbContext(options);
            });

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISchoolClassService, SchoolClassService>();
            services.AddScoped<ISchoolClassRepository, SchoolClassRepository>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IParentRepository, ParentRepository>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IDailyScheduleRepository, DailyScheduleRepository>();
            services.AddScoped<IDailyScheduleService, DailyScheduleService>();
            services.AddScoped<IWeeklyScheduleRepository, WeeklyScheduleRepository>();
            services.AddScoped<IWeeklyScheduleService, WeeklyScheduleService>();

            services.AddScoped<IScheduleInfoProvider, ScheduleInfoProvider>();

            services.AddAutoMapper(typeof(MappingProfile));
        }

        private static void InitializeDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Edu_System_BackEndDbContext>();
                context.Database.Migrate();
                DataSeed.SeedData(context);
            }
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();
        }
    }
}
