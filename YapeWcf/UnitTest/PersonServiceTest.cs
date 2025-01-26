using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using Business.Services;
using Contracts.DTOs;
using DataAccess.Interfaces;

namespace PersonWebService.Tests
{
    [TestFixture]
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> _personRepositoryMock;
        private PersonService _personService;

        [SetUp]
        public void SetUp()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepositoryMock.Object);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new PersonService(null));
            Assert.That(ex.ParamName, Is.EqualTo("repository"));
        }

        [Test]
        public void GetPersonsByPhoneNumber_ShouldThrowArgumentException_WhenPhoneNumberIsNullOrEmpty()
        {
            // Arrange
            var invalidPhoneNumbers = new[] { null, "", "   " };

            foreach (var phoneNumber in invalidPhoneNumbers)
            {
                // Act & Assert
                var ex = Assert.Throws<Exception>(() =>
                    _personService.GetPersonsByPhoneNumber(phoneNumber));

                Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
                Assert.That(ex.InnerException.Message, Does.Contain("The phone number cannot be null or empty."));
            }
        }

        [Test]
        public void GetPersonsByPhoneNumber_ShouldThrowArgumentException_WhenPhoneNumberIsInvalid()
        {
            // Arrange
            var invalidPhoneNumber = "12345";

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                _personService.GetPersonsByPhoneNumber(invalidPhoneNumber));

            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
            Assert.That(ex.InnerException.Message, Does.Contain("The phone number must be a valid 10-digit number."));

        }

        [Test]
        public void GetPersonsByPhoneNumber_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
        {
            // Arrange
            var phoneNumber = "1234567890";

            _personRepositoryMock
                .Setup(repo => repo.GetByPhoneNumber(phoneNumber))
                .Returns((PersonResponse)null);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                _personService.GetPersonsByPhoneNumber(phoneNumber));

            Assert.That(ex.InnerException, Is.TypeOf<KeyNotFoundException>());
            Assert.That(ex.InnerException.Message, Is.EqualTo("No person found with the given phone number."));
        }        

        [Test]
        public void GetPersonsByPhoneNumber_ShouldReturnPerson_WhenPersonExists()
        {
            // Arrange
            var phoneNumber = "1234567890";
            var expectedPerson = new PersonResponse
            {
                CellPhoneNumber = phoneNumber,
                DocumentNumber = "12345678",
                DocumentType = "DNI",
                LastName = "Doe",
                Name = "John"
            };

            _personRepositoryMock
                .Setup(repo => repo.GetByPhoneNumber(phoneNumber))
                .Returns(expectedPerson);

            // Act
            var result = _personService.GetPersonsByPhoneNumber(phoneNumber);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CellPhoneNumber, Is.EqualTo(expectedPerson.CellPhoneNumber));
            Assert.That(result.DocumentNumber, Is.EqualTo(expectedPerson.DocumentNumber));
            Assert.That(result.Name, Is.EqualTo(expectedPerson.Name));
        }
        [Test]
        public void GetPersonsByPhoneNumber_ShouldThrowException_WhenPersonDoesNotExist()
        {
            // Arrange
            var phoneNumber = "0000000000";

            _personRepositoryMock
                .Setup(repo => repo.GetByPhoneNumber(phoneNumber))
                .Returns((PersonResponse)null);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                _personService.GetPersonsByPhoneNumber(phoneNumber));
            Assert.That(ex.InnerException, Is.TypeOf<KeyNotFoundException>());
            Assert.That(ex.InnerException.Message, Is.EqualTo("No person found with the given phone number."));
        }
    }
}
