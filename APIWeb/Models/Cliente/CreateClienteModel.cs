using System.ComponentModel.DataAnnotations;

namespace APIWeb.Models.Cliente
{
    public class CreateClienteModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        public DateTime DataNascimento { get; set; }
    }
}
