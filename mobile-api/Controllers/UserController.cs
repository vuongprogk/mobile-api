using Mapster;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.User;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
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
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(GetUsers)}");
                var response = new GlobalResponse()
                {
                    Data = _userService.GetUsers(),
                    Message = "Get users success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)} action: {nameof(GetUsers)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(GetUserById)}");
                var response = new GlobalResponse()
                {
                    Data = await _userService.GetUserById(id),
                    Message = "Get user by id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)} action: {nameof(GetUserById)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(CreateUser)}");
                var user = request.Adapt<User>();
                var response = new GlobalResponse()
                {
                    Data = await _userService.CreateUser(user),
                    Message = "Create user success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)} action: {nameof(CreateUser)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest update, [FromRoute] string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(UpdateUser)}");
                var user = update.Adapt<User>();
                var response = new GlobalResponse()
                {
                    Data = await _userService.UpdateUser(user, id),
                    Message = "Update user success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)} action: {nameof(UpdateUser)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
