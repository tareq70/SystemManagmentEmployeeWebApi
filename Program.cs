
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using SystemManagmentEmployeeWebApi.Controllers.Fake_Api;
using SystemManagmentEmployeeWebApi.Exceptions;
using SystemManagmentEmployeeWebApi.Models.Data;
using SystemManagmentEmployeeWebApi.Models.Entities;
using SystemManagmentEmployeeWebApi.Repositories;
using SystemManagmentEmployeeWebApi.Services;

namespace SystemManagmentEmployeeWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Authentication

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var jwtSection = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion


            builder.Services.AddControllers();

            #region Repositories and Services
            builder.Services.AddDbContext<AppDbContext>(option =>
               {
                   option.UseSqlServer(builder.Configuration.GetConnectionString("connectionString"));
               });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
            builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();
            builder.Services.AddHttpClient<IBankService, BankServices>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddHttpClient<IBankService, BankServices>()
                            .ConfigurePrimaryHttpMessageHandler(() =>
                            {
                                return new HttpClientHandler
                                {
                                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                                };
                            });
            #endregion


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await Seeding.SeedRoles.SeedRolesAsync(roleManager);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
