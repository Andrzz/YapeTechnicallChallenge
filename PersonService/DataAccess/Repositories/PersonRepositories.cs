using Contracts.Responses;
using System.Collections.Generic;

namespace PersonService.DataAccess
{
    public class PersonRepository
    {
        public List<PersonResponse> GetPeople()
        {
            return new List<PersonResponse>
            {
                new PersonResponse
                {
                    CellPhoneNumber = "123456789",
                    Name = "John",
                    LastName = "Doe",
                    DocumentType = "DNI",
                    DocumentNumber = "12345678"
                },
                new PersonResponse
                {
                    CellPhoneNumber = "987654321",
                    Name = "Jane",
                    LastName = "Smith",
                    DocumentType = "Passport",
                    DocumentNumber = "A987654"
                }
            };
        }
    }
}