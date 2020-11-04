using Microsoft.AspNetCore.Mvc;
using WXTechChallenge.Common.Dtos.Response;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<UserDto> GetUser()
        {
            return _userService.GetUser();
        }
    }
}
