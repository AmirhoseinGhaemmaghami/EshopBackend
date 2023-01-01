namespace EshopBackend.WebApi.Errors_Models
{
    public class ApiErrorModel
    {
        public ApiErrorModel(int StatusCode, string? Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message?? GetErrorMessage();
        }

        private string GetErrorMessage()
        {
            switch (StatusCode)
            {
                case 400:
                    return "Bad Request";
                case 401:
                    return "UnAuthorized";
                case 404:
                    return "Not Found";
                case 500:
                    return "Server Error";
                default:
                    return null;
            }
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
