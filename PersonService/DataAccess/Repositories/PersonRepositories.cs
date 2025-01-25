using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contracts.Responses;
using Newtonsoft.Json;

namespace PersonService.DataAccess
{
    public class PersonRepository
    {
        private readonly List<PersonResponse> _people;

        public PersonRepository()
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DataAccess", "Data", "Persons.json");
            if (File.Exists(jsonPath))
            {
                var jsonData = File.ReadAllText(jsonPath);
                _people = JsonConvert.DeserializeObject<List<PersonResponse>>(jsonData) ?? new List<PersonResponse>();
            }
            else
            {
                _people = new List<PersonResponse>();
            }
        }

        public List<PersonResponse> GetPeople()
        {
            return _people;
        }

        public PersonResponse GetPersonByPhoneNumber(string phoneNumber)
        {
            return _people.FirstOrDefault(p => p.CellPhoneNumber == phoneNumber);
        }
    }
}