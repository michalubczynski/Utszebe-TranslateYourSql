namespace API.Errors
{
    public class ApiExceptions : ApiResponse
    {
        public string Details { get; set; }
        public ApiExceptions(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
