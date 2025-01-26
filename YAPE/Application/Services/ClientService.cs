using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
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
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Client cannot be null.");
            }
            try
            {
                var response = await _personServiceClient.GetPersonsByPhoneNumberAsync(client.CellPhoneNumber);

                if (response == null || response.DocumentType != client.DocumentType || response.DocumentNumber != client.DocumentNumber)
                {
                    throw new InvalidOperationException("Validation failed with the SOAP service, DocumentType and DocumentNumber must match on the client ");
                }

                var validatedClient = ClientToValidatedClientMapper.MapClientToValidatedClient(response, client);
                SaveValidatedClientIntoDatabase(validatedClient);
                return validatedClient.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the client: {ex.Message}", ex);
            }
        }

        public async Task<List<YapeValidatedClient>> GetValidatedClientsAsync()
        {
            return await _context.YapeValidatedClients.ToListAsync();
        }

        private void SaveValidatedClientIntoDatabase(YapeValidatedClient validatedClient)
        {
            _context.YapeValidatedClients.Add(validatedClient);
            _context.SaveChanges();
        }
    }
}