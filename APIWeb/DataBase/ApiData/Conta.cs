using System.ComponentModel.DataAnnotations;

namespace APIWeb.DataBase.ApiData
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
