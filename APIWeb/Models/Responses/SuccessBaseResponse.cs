namespace WebAPI.Models.Responses
{
    public class SuccessBaseResponse
    {
        /// <example>true</example>
        public bool success { get; set; } = true;
        /// <example>Ok</example>
        public string message { get; set; } = "Ok";
    }
}
