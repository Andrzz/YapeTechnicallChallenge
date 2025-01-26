using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace YapeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] Client request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var clientId = await _clientService.CreateClientAsync(request);
                return Ok(new { Message = "User created successfully", ClientId = clientId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Extraer mensaje del SOAP desde la InnerException
                var soapErrorMessage = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { Error = soapErrorMessage });
            }
        }
        [HttpGet("GetValidatedClients")]
        public async Task<IActionResult> GetValidatedClients()
        {
            var clients = await _clientService.GetValidatedClientsAsync();
            return Ok(clients);
        }

    }
}