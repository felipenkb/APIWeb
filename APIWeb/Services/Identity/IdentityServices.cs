using APIWeb.Interfaces.Service;
using APIWeb.Models.Identity;
using APIWeb.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIWeb.Services.Identity
{
    public class IdentityServices : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        

        private readonly JwtOptions _jwtOptions;

        public IdentityServices(SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager,
                                IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
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
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

                await _userManager.AddToRoleAsync(identityUser, usuario.Role);
            }
               
            var usuarioCadastroResponse = new UsuarioCadastroResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
                usuarioCadastroResponse.AddErrors(result.Errors.Select(r => r.Description));

            return usuarioCadastroResponse;
        }

        public async Task<UsuarioLoginResponse> LoginUsuario(UsuarioLoginRequest usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);
            if (result.Succeeded)
                return await GerarToken(usuario.Email);

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
                    usuarioLoginResponse.AddError("Usuário ou senha estão incorretos");
            }

            return usuarioLoginResponse;
        }

        private async Task<UsuarioLoginResponse> GerarToken(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var tokenClaims = await ObterClaimsRoles(user);

            var dataExpiracao = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);
                
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UsuarioLoginResponse
            (
                success: true,
                token: token,
                dataExpiracao: dataExpiracao
            );
        }

        private async Task<IList<Claim>> ObterClaimsRoles(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}

