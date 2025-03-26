
using Edu_System_BackEnd.Edu_System_BackEnd.API.Middleware;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IRepositories;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IServices;
using Edu_System_BackEnd.Edu_System_BackEnd.Core.Mapping;
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Scopes for reps and servs
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ISchoolClassService, SchoolClassService>();
            builder.Services.AddScoped<ISchoolClassRepository, SchoolClassRepository>();
            builder.Services.AddScoped<IParentService, ParentService>();
            builder.Services.AddScoped<IParentRepository, ParentRepository>();


            builder.Services.AddAutoMapper(typeof(MappingProfile));


            //builder.Services.AddSingleton<AuditableInterceptor>();

            builder.Services.AddDbContext<Edu_System_BackEndDbContext>((provider, option) =>
            {
                //var interceptor = provider.GetRequiredService<AuditableInterceptor>();

                option.EnableSensitiveDataLogging()
                    .UseSqlite((builder.Configuration.GetConnectionString("DefaultConnection")), sqliteOption =>
                    {
                        //sqliteOption.MigrationsHistoryTable("__MyMigrationsHistory", "devtips_audit_logs");
                    })
                    //.AddInterceptors(interceptor)
                    .UseSnakeCaseNamingConvention();
            });



            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Edu_System_BackEndDbContext>();
                context.Database.Migrate();
                DataSeed.SeedData(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Middleware
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
