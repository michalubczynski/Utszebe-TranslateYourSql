namespace Utszebe.API.Helpers
{
    public class ResponseEnum
    {
        public enum Response
        {
            Success = 200,
            BadRequest = 400,
            BadAuthorization = 401,
            NoResource = 404,
            InternalError = 500
        }
    }
}
