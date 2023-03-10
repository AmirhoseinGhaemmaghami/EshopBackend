using EshopBackend.Shared.Dtos.Account;
using EshopBackend.Shared.Interfaces;
using EshopBackend.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("")]
        public async Task<ActionResult<LoginResultDto>> GetCurrentUser()
        {
            if(!this.HttpContext.User.Identity?.IsAuthenticated?? false)
                return ApiResponse.Unauthorized();
            var id = Convert.ToInt64(this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await userService.GetUserById(id);
            if (res != null)
            {
                return ApiResponse.Ok(res);
            }
            return ApiResponse.NotFound("User is not Available");
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
                return ApiResponse.Ok(res);
            }
            return ApiResponse.NotFound("User Not Found");
        }


        [HttpPost("Signout")]
        public async Task<ActionResult> Signout()
        {
            return Ok();
        }

        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult<bool>> ConfirmEmail([FromQuery]int userId, [FromQuery] string code)
        {
            return ApiResponse.Ok(await this.userService.ConfirmEmail(userId, code));
        }

        [HttpPut("")]
        [Authorize]
        public async Task<ActionResult<EditUserResultDto>> EditUser(string firstname, string lastname, string address) 
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userService.UpdateUser(Convert.ToInt64(userId), firstname, lastname, address);
            return ApiResponse.Ok(user);
        }
    }
}
