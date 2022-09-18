namespace WebAPI.Models.Responses
{
    public class FailedBaseResponse
    {
        /// <example>false</example>
        public bool success { get; set; } = false;
        /// <example>Error</example>
        public string message { get; set; } = "Error";
    }
}
