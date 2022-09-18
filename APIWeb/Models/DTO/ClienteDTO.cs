namespace APIWeb.Models.DTO
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string DataDeNascimento { get; set; }
    }
}
