using Mapster;
using Microsoft.AspNetCore.Mvc;
using mobile_api.Dtos.Ticket;
using mobile_api.Models;
using mobile_api.Responses;
using mobile_api.Services.Interface;
using System.Threading.Tasks;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/ticket")]
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
                var response = new GlobalResponse()
                {
                    Data = await _ticketService.GetTickets(),
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
                var response = new GlobalResponse()
                {
                    Data = await _ticketService.GetTicketById(id),
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
                _logger.LogInformation($"{nameof(TicketController)} action: {nameof(CreateTicket)}");
                var ticket = request.Adapt<Ticket>();
                var response = new GlobalResponse()
                {
                    Data = await _ticketService.CreateTicket(ticket),
                    Message = "Create ticket success",
                    StatusCode = 200
                };
                return new JsonResult(response);
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
