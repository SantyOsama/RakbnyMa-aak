using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RakbnyMa_aak.Behaviors;
using RakbnyMa_aak.CQRS.Drivers.RegisterDriver.Commands;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.Filters;
using RakbnyMa_aak.Mapping;
using RakbnyMa_aak.MiddleWares;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Implementations;
using RakbnyMa_aak.Repositories.Interfaces;
using RakbnyMa_aak.SeedData;
using RakbnyMa_aak.Services;
using RakbnyMa_aak.Services.Drivers;
using RakbnyMa_aak.Services.Users;
using RakbnyMa_aak.SignalR;
using RakbnyMa_aak.UOW;
using System.Text;

namespace RakbnyMa_aak
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped<INotificationService, NotificationService>();



            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>(); 
            builder.Services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();



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

            builder.Services.AddHttpContextAccessor();

            // 3. Add controllers and Swagger
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            /****** Swagger & sOpenAPI  ******/
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        }); // to support JWT

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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidIssuer = builder.Configuration["Jwt:Issuer"],
                         ValidateAudience = true,
                         ValidAudience = builder.Configuration["Jwt:Audience"],
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                         ),
                         ValidateLifetime = true
                     };
                 });
            /// .. AddSignalR;

            builder.Services.AddSignalR();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            //Seed the database with initial data
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await DbSeeder.SeedRolesAsync(roleManager);
            }

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



            app.MapHub<NotificationHub>("/notificationHub");
            // 8. Map Controllers
            app.MapControllers();

            app.Run();
        }
    }
}