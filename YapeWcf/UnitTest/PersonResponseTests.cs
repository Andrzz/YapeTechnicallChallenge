using Contracts.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class PersonResponseTests
    {
        [TestMethod]
        public void CellPhoneNumber_ShouldGetAndSet()
        {
            // Arrange
            var personResponse = new PersonResponse();
            var expected = "123-456-7890";

            // Act
            personResponse.CellPhoneNumber = expected;
            var actual = personResponse.CellPhoneNumber;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Name_ShouldGetAndSet()
        {
            // Arrange
            var personResponse = new PersonResponse();
            var expected = "John";

            // Act
            personResponse.Name = expected;
            var actual = personResponse.Name;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LastName_ShouldGetAndSet()
        {
            // Arrange
            var personResponse = new PersonResponse();
            var expected = "Doe";

            // Act
            personResponse.LastName = expected;
            var actual = personResponse.LastName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DocumentType_ShouldGetAndSet()
        {
            // Arrange
            var personResponse = new PersonResponse();
            var expected = "Passport";

            // Act
            personResponse.DocumentType = expected;
            var actual = personResponse.DocumentType;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DocumentNumber_ShouldGetAndSet()
        {
            // Arrange
            var personResponse = new PersonResponse();
            var expected = "A12345678";

            // Act
            personResponse.DocumentNumber = expected;
            var actual = personResponse.DocumentNumber;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
