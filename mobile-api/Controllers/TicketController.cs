using Microsoft.AspNetCore.Mvc;
using mobile_api.Services.Interface;

namespace mobile_api.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController: ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;
        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }
        [HttpGet("GetTickets")]
        public IActionResult GetTickets()
        {
            return Ok();
        }
        [HttpGet("GetByTicketId/{id}")]
        public IActionResult GetTicketById(string id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateTicket()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateTicket()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteTicket()
        {
            return Ok();
        }

    }
}
