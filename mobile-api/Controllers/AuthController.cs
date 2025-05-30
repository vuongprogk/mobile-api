﻿using System.Text.Json;
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
                var user = await _authService.GetUserByUsernameAsync(request.Username);
                if (user == null)
                {
                    return new JsonResult(new GlobalResponse()
                    {
                        Message = "User not found",
                        StatusCode = 404
                    });
                }

                var token = await _authService.LoginAsync(request);
                if (string.IsNullOrEmpty(token))
                {
                    return new JsonResult(new GlobalResponse()
                    {
                        Message = "Login failed",
                        StatusCode = 401
                    });
                }
                var userInfo = new
                {
                    user.Username,
                    user.Role,
                    Theme = "Dark"
                };
                var cookieValue = JsonSerializer.Serialize(userInfo);
                Response.Cookies.Append("userInfo", cookieValue, new CookieOptions
                {
                    HttpOnly = false, // Set to true if you don't want frontend JS to access it
                    Secure = true, // Use HTTPS
                    SameSite = SameSiteMode.None, // Required for cross-site cookies
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

                Response.Cookies.Append("auth", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
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
                Response.Cookies.Delete("userInfo");
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
