using Shared.Responses;

namespace Infrastructure.Interfaces
{
    public interface IClientServiceAdapter
    {
        Task<GetPersonsByPhoneNumberResult> GetPersonsByPhoneNumberAsync(string cellPhoneNumber);
    }
}
