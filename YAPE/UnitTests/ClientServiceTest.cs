
using Application.Services;
using Domain.Entities;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shared.Responses;

namespace UnitTests
{
    [TestFixture]
    public class ClientServiceTests : IDisposable
    {
        private Mock<IClientServiceAdapter> _mockAdapter;
        private ApplicationDbContext _dbContext;
        private ClientService _service;

        [SetUp]
        public void SetUp()
        {
            _mockAdapter = new Mock<IClientServiceAdapter>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _service = new ClientService(_mockAdapter.Object, _dbContext, Mock.Of<Microsoft.Extensions.Logging.ILogger<ClientService>>());
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateClientAsync_ShouldCreateClient_WhenValidDataProvided()
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

            var soapResponse = new GetPersonsByPhoneNumberResult
            {
                Name = "Jane",
                LastName = "Smith",
                CellPhoneNumber = "9876543210",
                DocumentType = "Passport",
                DocumentNumber = "A987654"
            };

            _mockAdapter.Setup(x => x.GetPersonsByPhoneNumberAsync(client.CellPhoneNumber))
                .ReturnsAsync(soapResponse);

            // Act
            var result = await _service.CreateClientAsync(client);

            // Assert
            Assert.That(result, Is.Not.EqualTo(Guid.Empty));
            var savedClient = await _dbContext.YapeValidatedClients.FirstOrDefaultAsync(x => x.Id == result);
            Assert.That(savedClient, Is.Not.Null);
            Assert.That(savedClient.Name, Is.EqualTo("Jane"));
        }

        [Test]
        public void CreateClientAsync_ShouldThrowException_WhenValidationFails()
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

            _mockAdapter.Setup(x => x.GetPersonsByPhoneNumberAsync(client.CellPhoneNumber))
                .ReturnsAsync((GetPersonsByPhoneNumberResult)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.CreateClientAsync(client));
            Assert.That(ex.Message, Does.Contain("Validation failed with the SOAP service"));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
