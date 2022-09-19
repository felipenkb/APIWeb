namespace APIWeb.Models.Responses
{
    public class FailedBaseResponse
    {
        public bool success { get; private set; } = false;
        public string message { get; private set; } = "Error";
    }
}
