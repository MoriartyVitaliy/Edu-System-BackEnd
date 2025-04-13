
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;


namespace Edu_System_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            //DB
            ConfigureDatabase(builder.Services, builder.Configuration);

            //JWT
            ConfigureJwt(builder.Services, builder.Configuration);

            //Services and Repositories
            ConfigureServicesRepository(builder.Services, builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });



            var app = builder.Build();

            app.UseCors("AllowReactApp");


            InitializeDatabase(app);

            ConfigurePipeline(app);

            app.Run();            
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddSingleton<Edu_System_BackEndDbContext>(provider =>
            {
                var options = new DbContextOptionsBuilder<Edu_System_BackEndDbContext>()
                    .UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                    .UseSnakeCaseNamingConvention()
                    .Options;
                return new Edu_System_BackEndDbContext(options);
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("RedisConnectionString");
                options.InstanceName = "EduSystem_";
            });
        }

        private static void ConfigureServicesRepository(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IScheduleInfoProvider, ScheduleInfoProvider>();
            services.AddScoped<ILoginAttemptProvider, LoginAttemptProvider>();

            services.AddAutoMapper(typeof(MappingProfile));
        }

        private static async void InitializeDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Edu_System_BackEndDbContext>();
                context.Database.Migrate();
                DataSeed.SeedData(context);

                var adminService = scope.ServiceProvider.GetRequiredService<IAdminService>();
                await adminService.InitializeAdminAsync();
            }
        }

        private static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            //.AddGoogle("Google", options =>
            //{
            //    IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");

            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //    options.SignInScheme = JwtBearerDefaults.AuthenticationScheme;
            //});

            services.AddAuthorization();
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();
        }
    }
}
