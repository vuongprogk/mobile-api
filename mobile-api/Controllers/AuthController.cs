using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.User;
using mobile_api.Interfaces;
using mobile_api.Responses;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation($"{nameof(AuthController)} action: {nameof(Login)} param {request}");
            if (request.Username == "admin" && request.Password == "admin")
            {
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
            return Unauthorized();
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            return Ok();
        }
    }
}
