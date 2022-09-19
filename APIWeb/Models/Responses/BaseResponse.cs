namespace APIWeb.Models.Responses
{
    public class BaseResponse
    {
        public bool success { get; set; } = true;
        public string message { get; set; } = "Ok";
    }
}
