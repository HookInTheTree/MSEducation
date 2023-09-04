using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MSEducation.Gateaway.Data;
using MSEducation.Gateaway.Services;
using MSEducation.AuthenticationManager;
using System.Text;

namespace MSEducation.Gateaway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();

            


            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 7;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<JwtAuthenticationOptions>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddCustomJWTAuthentication(builder.Configuration.GetSection("JWT").Get<JwtAuthenticationOptions>());

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("JWT", policy =>
                {
                    policy.AuthenticationSchemes.Add("OAuth");
                    policy.RequireAuthenticatedUser();
                });
            });

            builder.Services.AddScoped<JWTService>();

            

            var app = builder.Build();

             
             if (app.Environment.EnvironmentName == "Development")
            {
                app.Services.GetService<ApplicationDbContext>()?.Database.Migrate();
            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());

            app.Run();
        }
    }
}