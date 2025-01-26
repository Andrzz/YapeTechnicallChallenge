using Contracts.DTOs;
using DataAccess.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _jsonFilePath;

        public PersonRepository(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentException("The JSON file path cannot be null or empty.", nameof(jsonFilePath));
            }

            _jsonFilePath = jsonFilePath;
        }

        public PersonResponse GetByPhoneNumber(string cellPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(cellPhoneNumber))
            {
                throw new ArgumentException("The phone number cannot be null or empty.", nameof(cellPhoneNumber));
            }

            var people = LoadPeopleFromJson();

            if (!people.Any())
            {
                throw new InvalidOperationException("The JSON file is empty or contains invalid data.");
            }

            var person = people.FirstOrDefault(p => p.CellPhoneNumber == cellPhoneNumber);

            if (person != null && (string.IsNullOrWhiteSpace(person.DocumentType) || string.IsNullOrWhiteSpace(person.DocumentNumber)))
            {
                throw new InvalidOperationException("The person's data in the JSON file is incomplete.");
            }

            return person;
        }

        private List<PersonResponse> LoadPeopleFromJson()
        {
            try
            {
                if (!File.Exists(_jsonFilePath))
                {
                    throw new FileNotFoundException($"The JSON file was not found at {_jsonFilePath}");
                }

                var jsonData = File.ReadAllText(_jsonFilePath);

                var people = JsonConvert.DeserializeObject<List<PersonResponse>>(jsonData);

                return people ?? new List<PersonResponse>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error deserializing the JSON file. Ensure it has a valid format.", ex);
            }
        }
    }
}
