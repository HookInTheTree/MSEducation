using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MSEducation.AuthenticationManager
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomJWTAuthentication(this IServiceCollection services, JwtAuthenticationOptions authenticationOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var key = authenticationOptions.Key;
                var secureBytes = Encoding.UTF8.GetBytes(key);
                var signingKey = new SymmetricSecurityKey(secureBytes);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticationOptions.Issuer,
                    ValidAudience = authenticationOptions.Audience,
                    IssuerSigningKey = signingKey
                };
            });

            return services;
        }
    }
}