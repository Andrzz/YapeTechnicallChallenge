using Contracts.DTOs;

namespace DataAccess.Interfaces
{
    public interface IPersonRepository
    {
        PersonResponse GetByPhoneNumber(string cellPhoneNumber);
    }
}
