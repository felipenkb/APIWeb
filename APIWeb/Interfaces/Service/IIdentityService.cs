using APIWeb.Models.Identity;

namespace APIWeb.Interfaces
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuario);
        Task<UsuarioLoginResponse> LoginUsuario(UsuarioLoginRequest usuario);
    }
}
