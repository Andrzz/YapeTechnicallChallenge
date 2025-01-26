using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientServiceAdapter _personServiceClient;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientServiceAdapter personServiceClient, ApplicationDbContext context, ILogger<ClientService> logger)
        {
            _personServiceClient = personServiceClient;
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> CreateClientAsync(Client client)
        {
            _logger.LogInformation("Starting CreateClientAsync");

            if (client == null)
            {
                _logger.LogError("Client is null.");
                throw new ArgumentNullException(nameof(client), "Client cannot be null.");
            }

            try
            {
                _logger.LogInformation($"Validating client with phone number: {client.CellPhoneNumber}");

                var response = await _personServiceClient.GetPersonsByPhoneNumberAsync(client.CellPhoneNumber);

                if (response == null || response.DocumentType != client.DocumentType || response.DocumentNumber != client.DocumentNumber)
                {
                    _logger.LogWarning($"Validation failed for client with phone number: {client.CellPhoneNumber}");
                    throw new InvalidOperationException("Validation failed with the SOAP service, DocumentType and DocumentNumber must match on the client.");
                }

                _logger.LogInformation($"Validation succeeded for client: {response.Name} {response.LastName}");

                var validatedClient = ClientToValidatedClientMapper.MapClientToValidatedClient(response, client);

                _logger.LogInformation($"Saving validated client to database: {validatedClient.Id}");
                SaveValidatedClientIntoDatabase(validatedClient);

                _logger.LogInformation($"Client created successfully with ID: {validatedClient.Id}");
                return validatedClient.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating the client: {ex.Message}", ex);
                throw new Exception($"An error occurred while creating the client: {ex.Message}", ex);
            }
        }

        public async Task<List<YapeValidatedClient>> GetValidatedClientsAsync()
        {
            _logger.LogInformation("Fetching all validated clients from the database.");
            try
            {
                var clients = await _context.YapeValidatedClients.ToListAsync();
                _logger.LogInformation($"Fetched {clients.Count} validated clients.");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching validated clients.", ex);
                throw;
            }
        }

        private void SaveValidatedClientIntoDatabase(YapeValidatedClient validatedClient)
        {
            try
            {
                _logger.LogInformation($"Saving client with ID: {validatedClient.Id} into database.");
                _context.YapeValidatedClients.Add(validatedClient);
                _context.SaveChanges();
                _logger.LogInformation($"Client with ID: {validatedClient.Id} saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while saving the validated client into the database.", ex);
                throw;
            }
        }
    }
}
