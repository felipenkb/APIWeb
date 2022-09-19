using System.ComponentModel.DataAnnotations;

namespace APIWeb.Models.Identity
{
    public class UsuarioCadastroRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [EmailAddress(ErrorMessage = "O campo {0} é invalido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [StringLength(15, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais")]
        public string SenhaConfirmacao { get; set; }
        public string Role { get; set; }
    }
}
