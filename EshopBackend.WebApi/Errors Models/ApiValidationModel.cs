namespace EshopBackend.WebApi.Errors_Models
{
    public class ApiValidationModel : ApiErrorModel
    {
        public List<string> Errors { get; set; } = new List<string>();
        public ApiValidationModel() : base(400)
        {
        }
    }
}
