using APIWeb.Interfaces;
using APIWeb.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace APIWeb.Services.Identity
{
    public class IdentityServices : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        //private readonly JwtOptions _jwtOptions { get; set; }

        public IdentityServices(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager /*IOptions<JwtOptions> jwtOptions*/)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //_jwtOptions = jwtOptions;
        }

        public async Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuario)
        {
            var identityUser = new IdentityUser()
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, usuario.Senha);
            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

            var usuarioCadastroResponse = new UsuarioCadastroResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
                usuarioCadastroResponse.AddErrors(result.Errors.Select(r => r.Description));

            return usuarioCadastroResponse;
        }

        public async Task<UsuarioLoginResponse> LoginUsuario(UsuarioLoginRequest usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);
           // if (result.Succeeded)
               // return await GerarToken(usuario.Email);

            var usuarioLoginResponse = new UsuarioLoginResponse(result.Succeeded);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    usuarioLoginResponse.AddError("Conta bloqueada");
                else if (result.IsNotAllowed)
                    usuarioLoginResponse.AddError("Conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    usuarioLoginResponse.AddError("Necessario confirmar a autenticação de dois fatores");
                else
                    usuarioLoginResponse.AddError("Usuario ou senha estão incorretos");
            }

            return usuarioLoginResponse;
        }
    }
}

