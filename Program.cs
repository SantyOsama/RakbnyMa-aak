using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.CQRS.Drivers.RegisterDriver.Commands;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.Mapping;
using RakbnyMa_aak.MiddleWares;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Implementations;
using RakbnyMa_aak.Repositories.Interfaces;
using RakbnyMa_aak.Services;
using RakbnyMa_aak.Services.Drivers;
using RakbnyMa_aak.Services.Users;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionString")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Configuration.GetSection("Cloudinary");


            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<IDriverVerificationService, DriverVerificationService>();
            builder.Services.AddHttpClient<IDriverVerificationService, DriverVerificationService>();
            builder.Services.AddMediatR(typeof(RegisterDriverCommand).Assembly);
            builder.Services.AddScoped<IDriverRegistrationService, DriverRegistrationService>();
            builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtService, JwtService>();


            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            // 2. Configure Identity with ApplicationUser (NOT IdentityUser)
            builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // optional: password, lockout, etc.
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // 3. Add controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 4. Optional: Enable CORS (if frontend will call the API)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<GlobalErrorHandlerMiddleware>();
            builder.Services.AddScoped<TransactionMiddleware>();

            var app = builder.Build();

            // 5. Use Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // 6. Use CORS
            app.UseCors("AllowAll");


            //Add custom middlewares

            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();

            // 7. Use Authentication and Authorization
            app.UseAuthentication(); // important for Identity
            app.UseAuthorization();

            // 8. Map Controllers
            app.MapControllers();

            app.Run();
        }
    }
}