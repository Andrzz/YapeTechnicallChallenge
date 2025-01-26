using Domain.Entities;
using Shared.Responses;

namespace Application.Mappers
{
    public static class ClientToValidatedClientMapper
    {
        public static YapeValidatedClient MapClientToValidatedClient(GetPersonsByPhoneNumberResult response, Client client)
        {
            return new YapeValidatedClient() 
            { 
                CellPhoneNumber = response.CellPhoneNumber,
                DocumentNumber = response.DocumentNumber, 
                DocumentType = response.DocumentType, 
                Id = Guid.NewGuid(),
                LastName = response.LastName, 
                Name = response.Name,
                ReasonOfUse = client.ReasonOfUse
            };
        }
    }
}
