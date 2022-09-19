using APIWeb.Models.Responses;
using System.Text.Json.Serialization;

namespace APIWeb.Models.Identity
{
    public class UsuarioLoginResponse
    {
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DataExpiracao { get; set; }
        public List<string> Errors { get; set; }

        public UsuarioLoginResponse(bool success, string token, DateTime? dataExpiracao) : this(success)
        {
            Token = token;
            DataExpiracao = dataExpiracao;
        }

        public UsuarioLoginResponse() => Errors = new List<string>();

        public UsuarioLoginResponse(bool success = true) : this() => Success = success;

        public void AddError(string error) => Errors.Add(error);
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}
