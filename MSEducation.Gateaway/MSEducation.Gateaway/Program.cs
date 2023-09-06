using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MSEducation.AuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

namespace MSEducation.Gateaway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional:false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddOcelot(builder.Configuration);

            var jwtSettings = builder.Configuration.GetSection("JWT")
                .Get<JwtAuthenticationOptions>();

            if (jwtSettings == null)
                throw new ArgumentNullException("JWT section in appsettings.json wasn't defined!");

            builder.Services.AddCustomJWTAuthentication(jwtSettings);
            
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            await app.UseOcelot();

            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}