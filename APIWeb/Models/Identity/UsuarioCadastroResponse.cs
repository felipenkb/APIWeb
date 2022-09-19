using APIWeb.Models.Responses;

namespace APIWeb.Models.Identity
{
    public class UsuarioCadastroResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public UsuarioCadastroResponse() => Errors = new List<string>();

        public UsuarioCadastroResponse(bool success = true) : this() => Success = success;

        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}
