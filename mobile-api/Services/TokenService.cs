using Microsoft.IdentityModel.Tokens;
using mobile_api.Models;
using mobile_api.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mobile_api.Services
{
    public class TokenService : ITokenService
    {
        public string? CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Jv4;_^w^V/i{$fn;&FgRC6kSX'KHdZr8`of^=.&qSnKXi5[D>I'!4lYElpFn1\"-");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                ]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
          
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Jv4;_^w^V/i{$fn;&FgRC6kSX'KHdZr8`of^=.&qSnKXi5[D>I'!4lYElpFn1\"-");
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out  _);

            var username = principal.Identity?.Name; // ClaimTypes.Name
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = principal.FindFirst(ClaimTypes.Role)?.Value;
            Console.WriteLine($"User: {username}, ID: {userId}, Role: {role}");
            

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                return null;
            }
            return principal;
        }
    }
}
