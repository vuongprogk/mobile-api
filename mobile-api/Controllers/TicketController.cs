using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.Ticket;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;
using System.Security.Claims;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;
        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        [HttpGet("GetTickets")]
        public async Task<IActionResult> GetTickets()
        {
            try
            {
                _logger.LogInformation($"{nameof(TicketController)} action: {nameof(GetTickets)}");
                var isAdmin = User.IsInRole("Admin");
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var response = new GlobalResponse()
                {
                    Data = isAdmin ? await _ticketService.GetTickets() : await _ticketService.GetTicketsByUserId(userId),
                    Message = "Get tickets success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TicketController)} action: {nameof(GetTickets)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpGet("GetByTicketId/{id}")]
        public async Task<IActionResult> GetTicketById(string id)
        {
            try
            {
                _logger.LogInformation($"{nameof(TicketController)} action: {nameof(GetTicketById)}");
                var isAdmin = User.IsInRole("Admin");
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var ticket = await _ticketService.GetTicketById(id);
                
                if (ticket == null)
                {
                    return NotFound(new GlobalResponse()
                    {
                        Message = "Ticket not found",
                        StatusCode = 404
                    });
                }

                if (!isAdmin && ticket.UserId != userId)
                {
                    return Forbid();
                }

                var response = new GlobalResponse()
                {
                    Data = ticket,
                    Message = "Get ticket by id success",
                    StatusCode = 200
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TicketController)} action: {nameof(GetTicketById)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Invalid request data",
                        StatusCode = 400,
                        Data = ModelState.Values.SelectMany(v => v.Errors)
                    });
                }

                _logger.LogInformation($"{nameof(TicketController)} action: {nameof(CreateTicket)}");
                
                // Get user ID from token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError("User ID not found in token");
                    return Unauthorized(new GlobalResponse()
                    {
                        Message = "User ID not found in token",
                        StatusCode = 401
                    });
                }

                _logger.LogInformation($"Creating ticket for user ID: {userId}");

                var ticket = new Ticket
                {
                    Title = request.Title,
                    AgeGroup = request.AgeGroup,
                    Price = request.Price,
                    Description = request.Description,
                    Quantity = request.Quantity,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _ticketService.CreateTicket(ticket);
                if (!result)
                {
                    return BadRequest(new GlobalResponse()
                    {
                        Message = "Failed to create ticket",
                        StatusCode = 400
                    });
                }

                var response = new GlobalResponse()
                {
                    Data = ticket,
                    Message = "Create ticket success",
                    StatusCode = 201
                };

                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TicketController)} action: {nameof(CreateTicket)} error");
                return StatusCode(500, new GlobalResponse()
                {
                    Message = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}
