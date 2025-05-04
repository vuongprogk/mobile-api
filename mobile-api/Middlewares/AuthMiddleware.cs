using mobile_api.Services.Interface;

namespace mobile_api.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;
    private readonly ITokenService _tokenService;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger, ITokenService tokenService)
    {
        _next = next;
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"{nameof(AuthMiddleware)} action: {nameof(InvokeAsync)}");
        var token = context.Request.Cookies["auth"];
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var user = _tokenService.ValidateToken(token);
                if (user != null)
                {
                    context.User = user;
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the AuthMiddleware");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred while processing your request.");
                return;
            }
        }

        await _next(context);
    }
}