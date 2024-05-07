using backend.Entities.User;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace backend.Utils
{
    public class JwtProvider
    {
        private readonly string _securityKey;
        public JwtProvider() {

            DotNetEnv.Env.Load();
            _securityKey = Environment.GetEnvironmentVariable("SecurityKey");
;        }

        public string GenerateToken(User userData)
        {
            Claim[] claims =
            {
                new("email", userData.Email),
                new("password", userData.Password)
            };

            var signCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signCredentials,
                expires: DateTime.UtcNow.AddDays(1)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<Claim> DecodeToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_securityKey);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var claimsPrincipal = handler.ValidateToken(token, validations, out var tokenSecure);
                return new List<Claim>(claimsPrincipal.Claims);
            }
            catch (SecurityTokenValidationException)
            {
                return new List<Claim>();
            }
        }

    }
}
