using static Utszebe.Infrastructure.Translation.ResponseEnum;

namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        public string GetDefaultMessage(int statusCode)
        {
            if (statusCode == 0)
                return string.Empty;

            return statusCode switch
            {
                (int)Response.BadRequest => "Bad request encountered",
                (int)Response.BadAuthorization => "Not authorized",
                (int)Response.NoResource => "No resource was found",
                (int)Response.InternalError => "Internal server error",
                _ => null
            } ?? "Null reference";
        }
    }
}
