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
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation($"{nameof(AuthController)} action: {nameof(Login)} param {request}");
                var token = await _authService.LoginAsync(request);
                if (string.IsNullOrEmpty(token))
                {
                    return new JsonResult(new GlobalResponse()
                    {
                        Message = "Login failed",
                        StatusCode = 401
                    });
                }
                Response.Cookies.Append("auth", token, new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                });
                var response = new GlobalResponse()
                {
                    Message = "Login success",
                    StatusCode = 200
                };

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(AuthController)} action: {nameof(Login)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                _logger.LogInformation($"{nameof(AuthController)} action: {nameof(Register)} param {request}");
                var response = new GlobalResponse()
                {
                    Data = await _authService.RegisterAsync(request),
                    Message = "Register success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(AuthController)} action: {nameof(Register)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("auth");
                var response = new GlobalResponse()
                {
                    Message = "Logout success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(AuthController)} action: {nameof(Register)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
