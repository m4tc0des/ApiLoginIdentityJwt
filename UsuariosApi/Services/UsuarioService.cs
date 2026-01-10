
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(UserManager<Usuario> userManager, IMapper mapper = null, SignInManager<Usuario> signInManager = null, TokenService tokenService = null)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            return await _userManager.CreateAsync(usuario, dto.Password);
        }

        public async Task<SignInResult> Login(LoginUsuarioDto dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            _tokenService.GenerateToken(usuario);
        }
        
    }
}
