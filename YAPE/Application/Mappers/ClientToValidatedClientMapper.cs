using Domain.Entities;
using Shared.Responses;

namespace Application.Mappers
{
    public static class ClientToValidatedClientMapper
    {
        public static YapeValidatedClient MapClientToValidatedClient(PersonResponse client)
        {
            return new YapeValidatedClient() 
            { 
                CellPhoneNumber = client.CellPhoneNumber,
                DocumentNumber = client.DocumentNumber, 
                DocumentType = client.DocumentType, 
                Id = Guid.NewGuid(),
                LastName = client.LastName, 
                Name = client.Name,     
            };
        }
    }
}
