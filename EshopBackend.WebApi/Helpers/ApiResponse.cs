using EshopBackend.WebApi.Errors_Models;
using Microsoft.AspNetCore.Mvc;

namespace EshopBackend.WebApi.Helpers
{
    public static class ApiResponse
    {
        public static OkObjectResult Ok(object obj)
        {
            return new OkObjectResult(obj);
        }

        public static NotFoundObjectResult NotFound(string? message = null)
        {
            return new NotFoundObjectResult(new ApiErrorModel(404, message));
        }

        public static UnauthorizedObjectResult Unauthorized(string? message = null)
        {
            return new UnauthorizedObjectResult(new ApiErrorModel(401));
        }

        public static BadRequestObjectResult BadRequest(string? message = null)
        {
            return new BadRequestObjectResult(new ApiErrorModel(400, message));
        }

        public static BadRequestObjectResult BadRequest(List<string> Errors)
        {
            return new BadRequestObjectResult(
                new ApiValidationModel() { Errors = Errors});
        }
    }
}
