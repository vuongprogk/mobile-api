using Mapster;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(GetUsers)}");
                var response = new GlobalResponse()
                {
                    Data = await _userService.GetUsers(),
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(CreateUser)}");
                var user = request.Adapt<User>();
                // hash password
                user.HashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
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
        [HttpPut("UpdateUser/{id}")]
        [Authorize(Roles = "Admin")]
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
        [HttpPut("UpdateUserRole/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole([FromRoute] string id, [FromBody] Role newRole)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(UpdateUserRole)}");
                var response = new GlobalResponse()
                {
                    Data = await _userService.UpdateUserRoleAsync(id, newRole),
                    Message = "Update user role success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)} action: {nameof(UpdateUserRole)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpGet("GetUsersByRole/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByRole([FromRoute] Role role)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserController)} action: {nameof(GetUsersByRole)}");
                var response = new GlobalResponse()
                {
                    Data = await _userService.GetUsersByRoleAsync(role),
                    Message = "Get users by role success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserController)} action: {nameof(GetUsersByRole)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
