
using System.Security.Claims;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    internal class TokenService
    {
        public void GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
            };
        }
    }
}