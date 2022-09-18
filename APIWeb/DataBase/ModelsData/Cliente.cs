using System.ComponentModel.DataAnnotations;

namespace WebAPI.DataBase.ModelsData
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}
