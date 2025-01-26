using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<Guid> CreateClientAsync(Client client);
        Task<List<YapeValidatedClient>> GetValidatedClientsAsync();
    }
}
