using EshopBackend.Shared.Dtos.Account;
using EshopBackend.Shared.Interfaces;
using EshopBackend.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EshopBackend.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResultDto>> Register([FromBody] RegisterInputDto signInInputDto)
        {
            var res = await userService.Register(signInInputDto);
            return ApiResponse.Ok(res);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResultDto>> LogIn([FromBody] LoginInputDto loginInputDto)
        {
            var res = await userService.Login(loginInputDto);
            if (res != null)
            {
                if (!res.IsActivated)
                {
                    return ApiResponse.BadRequest("User is not activated");
                }
                return ApiResponse.Ok(res);
            }
            return ApiResponse.NotFound("User Not Found");
        }


        [HttpPost("Signout")]
        public async Task<ActionResult> Signout([FromBody] string value)
        {
            if (User?.Identity != null)
                if (User.Identity.IsAuthenticated)
                {
                    await this.HttpContext.SignOutAsync();
                }
            return Ok();
        }
    }
}
