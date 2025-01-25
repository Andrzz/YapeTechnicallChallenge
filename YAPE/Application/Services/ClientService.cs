using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Infrastructure.DataBase;
using Shared.Interfaces;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IPersonServiceClient _personServiceClient;
        private readonly ApplicationDbContext _context;

        public ClientService(IPersonServiceClient personServiceClient, ApplicationDbContext context)
        {
            _personServiceClient = personServiceClient;
            _context = context;
        }

        public async Task<Guid> CreateClientAsync(Client client)
        {
            var response = await _personServiceClient.GetPersonsByPhoneNumberAsync(client.CellPhoneNumber);

            if (response == null || response.DocumentType != client.DocumentType || response.DocumentNumber != client.DocumentNumber)
            {
                throw new InvalidOperationException("Validation failed with the SOAP service.");
            }

            var validatedClient = ClientToValidatedClientMapper.MapClientToValidatedClient(response);

            //client.Id = Guid.NewGuid();
            //_context.Clients.Add(client);
            //await _context.SaveChangesAsync();

            return validatedClient.Id;
        }
    }
}