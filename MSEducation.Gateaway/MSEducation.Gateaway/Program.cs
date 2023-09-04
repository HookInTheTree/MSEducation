using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MSEducation.AuthenticationManager;
using System.Text;

namespace MSEducation.Gateaway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());

            app.Run();
        }
    }
}