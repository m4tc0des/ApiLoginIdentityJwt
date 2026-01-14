using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration? configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString("yyyy-MM-dd")),
                new Claim("loginTimeStamp", DateTime.UtcNow.ToString("o")) // formato ISO 8601
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
            var creds = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "UsuariosApi",
                audience: "UsuariosApiClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
