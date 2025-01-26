using Contracts.DTOs;
using Contracts.Interfaces;
using DataAccess.Interfaces;

namespace Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public PersonResponse GetPersonsByPhoneNumber(string cellPhoneNumber)
        {
            return _repository.GetByPhoneNumber(cellPhoneNumber);
        }
    }
}
