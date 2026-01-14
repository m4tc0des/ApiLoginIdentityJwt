using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {

        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> AdicionarUsuario(CreateUsuarioDto dto)
        {
            var result = await _usuarioService.Cadastra(dto);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Erro ao cadastrar usuário: {errors}");
            }

            return Ok("Usuario Cadastrado.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok(token);

        }
    }
}
