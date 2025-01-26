using Shared.Responses;

namespace Shared.Interfaces
{
    public interface IPersonServiceClient
    {
        Task<GetPersonsByPhoneNumberResult> GetPersonsByPhoneNumberAsync(string cellPhoneNumber);
    }
}
