using Contracts.Responses;
using PersonService.Contracts;
using PersonService.DataAccess;
using System.Collections.Generic;

namespace PersonService.Business
{
    public class PersonService : IPersonService
    {
        private readonly PersonRepository _repository;

        public PersonService()
        {
            _repository = new PersonRepository();
        }

        public List<PersonResponse> GetPersonsByPhoneNumber(string cellPhoneNumber)
        {
            var people = _repository.GetPeople();
            return people.FindAll(p => p.CellPhoneNumber == cellPhoneNumber);
        }
    }
}