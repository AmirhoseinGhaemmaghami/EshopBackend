namespace EshopBackend.WebApi.Errors_Models
{
    public class ApiExceptionModel : ApiErrorModel
    {
        public ApiExceptionModel(int StatusCode, string ExceptionMessage) : base(StatusCode)
        {
            this.ExceptionMessage = ExceptionMessage;
        }

        public string ExceptionMessage { get; }
    }
}
