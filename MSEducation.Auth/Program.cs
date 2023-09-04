
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSEducation.AuthenticationManager.Services;
using MSEducation.AuthenticationManager;
using MSEducation.Auth.Data;

namespace MSEducation.Auth
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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["Database"]);
            });

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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.Services.GetService<ApplicationDbContext>()?.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}