using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountAPI2.Data
{
    public class JWTGenerator
    {
        private readonly ILogger<JWTGenerator> _logger;
        private readonly IConfiguration _configuration;

        public JWTGenerator(ILogger<JWTGenerator> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
        }
        public string GenerateToken(int userId, string role)
        {
            var jwtSettings = _configuration.GetSection("JWTSetting");

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings[""])),
                claims: claims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
