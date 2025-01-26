using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using YapeService.Controllers;

namespace UnitTests
{
    [TestFixture]
    public class ClientControllerTests
    {
        private Mock<IClientService> _mockService;
        private ClientController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IClientService>();
            _controller = new ClientController(_mockService.Object);
        }

        [Test]
        public async Task CreateClient_ShouldReturnOk_WhenClientIsCreatedSuccessfully()
        {
            // Arrange
            var client = new Client
            {
                Name = "Jane",
                LastName = "Smith",
                CellPhoneNumber = "9876543210",
                DocumentType = "Passport",
                DocumentNumber = "A987654",
                ReasonOfUse = "Traveling"
            };

            var generatedId = Guid.NewGuid();
            _mockService.Setup(x => x.CreateClientAsync(client)).ReturnsAsync(generatedId);

            // Act
            var result = await _controller.CreateClient(client) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value.ToString(), Does.Contain("User created successfully"));
        }

        [Test]
        public async Task CreateClient_ShouldReturnBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            var client = new Client
            {
                Name = "Invalid",
                LastName = "Client",
                CellPhoneNumber = "1111111111",
                DocumentType = "Passport",
                DocumentNumber = "InvalidDoc",
                ReasonOfUse = "Testing"
            };

            _mockService.Setup(x => x.CreateClientAsync(client)).ThrowsAsync(new InvalidOperationException("Validation failed"));

            // Act
            var result = await _controller.CreateClient(client) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value.ToString(), Does.Contain("Validation failed"));
        }
    }
}
