using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.User;
using mobile_api.Responses;
using mobile_api.Services.Interface;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        public AuthController(ILogger<AuthController> logger, ITokenService tokenService, IAuthService authService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation($"{nameof(AuthController)} action: {nameof(Login)} param {request}");
            var response = new GlobalResponse()
            {
                Status = StatusCodes.Status200OK,
                Message = "Login successful",
                Data = _tokenService.CreateToken(new Models.User()
                {
                    Username = request.Username,
                    Password = request.Password
                })
            };
            return new JsonResult(response);
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            return Ok();
        }
    }
}
