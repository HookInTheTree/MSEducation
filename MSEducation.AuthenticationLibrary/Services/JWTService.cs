using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MSEducation.AuthenticationManager.Services
{
    public class JWTService
    {
        private readonly JwtAuthenticationOptions options;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string key;

        public JWTService(IOptionsSnapshot<JwtAuthenticationOptions> _options, UserManager<IdentityUser> uManager) {
            options = _options.Value;
            userManager = uManager;
        }

        public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var key = options.Key;
            var issuer = options.Issuer;
            var audience = options.Audience;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
