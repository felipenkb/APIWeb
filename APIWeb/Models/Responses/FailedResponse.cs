namespace APIWeb.Models.Responses
{
    public class FailedResponse
    {
        /// <example>false</example>
        public bool success { get; private set; } = false;
        /// <example>Error</example>
        public string message { get; private set; } = "Error";
    }
}
