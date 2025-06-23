using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Models;

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

            // 2. Configure Identity with ApplicationUser (NOT IdentityUser)
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

            // 7. Use Authentication and Authorization
            app.UseAuthentication(); // important for Identity
            app.UseAuthorization();

            // 8. Map Controllers
            app.MapControllers();

            app.Run();
        }
    }
}
