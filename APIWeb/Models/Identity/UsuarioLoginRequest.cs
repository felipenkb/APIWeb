using System.ComponentModel.DataAnnotations;

namespace APIWeb.Models.Identity
{
    public class UsuarioLoginRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [EmailAddress(ErrorMessage = "O campo {0} é invalido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        public string  Senha { get; set; }
    }
}
