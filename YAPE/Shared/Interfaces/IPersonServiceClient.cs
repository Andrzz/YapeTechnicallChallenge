using Shared.Responses;

namespace Shared.Interfaces
{
    public interface IPersonServiceClient
    {
        Task<PersonResponse> GetPersonsByPhoneNumberAsync(string cellPhoneNumber);
    }
}
