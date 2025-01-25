using Domain.Entities;
using Infrastructure.DataBase;
using Infrastructure.Soap;
using Microsoft.AspNetCore.Mvc;

namespace YapeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SoapServiceClient _soapService;

        public ClientController(ApplicationDbContext context, SoapServiceClient soapService)
        {
            _context = context;
            _soapService = soapService;
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] Client request)
        {
            // Validar con el servicio SOAP
            var response = await _soapService.GetPersonsByPhoneNumberAsync(request.CellPhoneNumber);

            if (response == null || response.DocumentType != request.DocumentType || response.DocumentNumber != request.DocumentNumber)
            {
                return BadRequest("La validación con el servicio SOAP falló.");
            }


            //Guardar en la base de datos
            //request.Id = Guid.NewGuid();
            //_context.Clients.Add(request);
            //await _context.SaveChangesAsync();

            return Ok(new { Id = request.Id });
        }
    }
}
