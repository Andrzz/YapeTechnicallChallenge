using Contracts.DTOs;
using DataAccess.Repository;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace PersonWebService.Tests
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private string _testJsonFilePath;
        private PersonRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // Crear un archivo JSON temporal para las pruebas
            _testJsonFilePath = Path.GetTempFileName();

            File.WriteAllText(_testJsonFilePath, @"
            [
                {
                    ""CellPhoneNumber"": ""1234567890"",
                    ""DocumentNumber"": ""12345678"",
                    ""DocumentType"": ""DNI"",
                    ""LastName"": ""Doe"",
                    ""Name"": ""John""
                },
                {
                    ""CellPhoneNumber"": ""9876543210"",
                    ""DocumentNumber"": ""87654321"",
                    ""DocumentType"": ""Passport"",
                    ""LastName"": ""Smith"",
                    ""Name"": ""Jane""
                }
            ]");

            _repository = new PersonRepository(_testJsonFilePath);
        }

        [TearDown]
        public void TearDown()
        {
            // Eliminar el archivo JSON temporal después de cada prueba
            if (File.Exists(_testJsonFilePath))
            {
                File.Delete(_testJsonFilePath);
            }
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenFilePathIsInvalid()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new PersonRepository(null));
            Assert.That(ex.Message, Does.Contain("The JSON file path cannot be null or empty."));
        }

        [Test]
        public void GetByPhoneNumber_ShouldReturnPerson_WhenPersonExists()
        {
            // Act
            var result = _repository.GetByPhoneNumber("1234567890");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
        }

        [Test]
        public void GetByPhoneNumber_ShouldReturnNull_WhenPersonDoesNotExist()
        {
            // Act
            var result = _repository.GetByPhoneNumber("0000000000");

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetByPhoneNumber_ShouldThrowArgumentException_WhenPhoneNumberIsInvalid()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _repository.GetByPhoneNumber(null));
            Assert.That(ex.Message, Does.Contain("The phone number cannot be null or empty."));
        }

        [Test]
        public void GetByPhoneNumber_ShouldThrowFileNotFoundException_WhenJsonFileDoesNotExist()
        {
            // Arrange
            var invalidPath = "invalid.json";
            var repository = new PersonRepository(invalidPath);

            // Act & Assert
            var ex = Assert.Throws<FileNotFoundException>(() => repository.GetByPhoneNumber("1234567890"));
            Assert.That(ex.Message, Does.Contain($"The JSON file was not found at {invalidPath}"));
        }

        [Test]
        public void GetByPhoneNumber_ShouldThrowInvalidOperationException_WhenJsonIsEmpty()
        {
            // Arrange
            File.WriteAllText(_testJsonFilePath, "[]");

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _repository.GetByPhoneNumber("1234567890"));
            Assert.That(ex.Message, Does.Contain("The JSON file is empty or contains invalid data."));
        }

        [Test]
        public void GetByPhoneNumber_ShouldThrowInvalidOperationException_WhenJsonIsInvalid()
        {
            // Arrange
            File.WriteAllText(_testJsonFilePath, "{ invalid json }");

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _repository.GetByPhoneNumber("1234567890"));
            Assert.That(ex.Message, Does.Contain("Error deserializing the JSON file."));
        }
    }
}
