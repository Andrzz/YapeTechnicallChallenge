using Contracts.DTOs;
using DataAccess.Interfaces;
using Newtonsoft.Json;
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
            _jsonFilePath = jsonFilePath;
        }

        public PersonResponse GetByPhoneNumber(string cellPhoneNumber)
        {
            // Leer y deserializar los datos del archivo JSON
            var people = LoadPeopleFromJson();

            // Buscar la persona por número de teléfono
            return people.FirstOrDefault(p => p.CellPhoneNumber == cellPhoneNumber);
        }

        private List<PersonResponse> LoadPeopleFromJson()
        {
            // Verifica si el archivo existe
            if (!File.Exists(_jsonFilePath))
            {
                return new List<PersonResponse>();
            }

            // Leer el contenido del archivo JSON
            var jsonData = File.ReadAllText(_jsonFilePath);

            // Deserializar el contenido a una lista de PersonResponse
            return JsonConvert.DeserializeObject<List<PersonResponse>>(jsonData) ?? new List<PersonResponse>();
        }
    }
}
