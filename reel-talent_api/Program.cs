using Microsoft.EntityFrameworkCore;
using reel_talent_api.Application.Interfaces.Repositories;
using reel_talent_api.Application.Interfaces.Services;
using reel_talent_api.Infrastructure.Persistence;
using reel_talent_api.Infrastructure.Persistence.Repositories;
using reel_talent_api.Infrastructure.Persistence.Services;

namespace reel_talent_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173") // Allow your frontend's URL
                              .AllowCredentials()  // Allow credentials (cookies, authorization headers)
                              .AllowAnyHeader()    // Allow any headers
                              .AllowAnyMethod();   // Allow any HTTP method
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
