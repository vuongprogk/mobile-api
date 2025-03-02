using Microsoft.AspNetCore.Mvc;
using mobile_api.Services.Interface;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok();
        }
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(string id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateUser()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateUser()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
    }
}
