namespace APIWeb.Models.Responses
{
    public class SuccessResponse
    {
        /// <example>true</example>
        public bool success { get; set; } = true;
        /// <example>Ok</example>
        public string message { get; set; } = "Ok";
    }
}
