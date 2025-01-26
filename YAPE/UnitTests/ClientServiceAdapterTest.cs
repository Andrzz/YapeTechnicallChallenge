using Infrastructure.Adapters;
using Moq;
using Moq.Protected;
using System.Net;

namespace Infrastructure.Tests
{
    [TestFixture]
    public class ClientServiceAdapterTests
    {
        private Mock<HttpMessageHandler> _mockHttpHandler;
        private ClientServiceAdapter _adapter;

        [SetUp]
        public void SetUp()
        {
            _mockHttpHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            var httpClient = new HttpClient(_mockHttpHandler.Object);

            _adapter = new ClientServiceAdapter("http://localhost:5000/soap")
            {
                _httpClient = httpClient 
            };
        }

        [Test]
        public async Task GetPersonsByPhoneNumberAsync_ShouldReturnResponse_WhenValidDataProvided()
        {
            // Arrange
            var soapResponse = @"
        <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
            <s:Body>
                <GetPersonsByPhoneNumberResponse xmlns=""http://tempuri.org/"">
                    <GetPersonsByPhoneNumberResult xmlns:a=""http://schemas.datacontract.org/2004/07/Contracts.DTOs"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
                        <a:CellPhoneNumber>1234567890</a:CellPhoneNumber>
                        <a:DocumentNumber>12345678</a:DocumentNumber>
                        <a:DocumentType>DNI</a:DocumentType>
                        <a:LastName>Doe</a:LastName>
                        <a:Name>John</a:Name>
                    </GetPersonsByPhoneNumberResult>
                </GetPersonsByPhoneNumberResponse>
            </s:Body>
        </s:Envelope>";

            _mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(soapResponse)
                });

            // Act
            var result = await _adapter.GetPersonsByPhoneNumberAsync("1234567890");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
            Assert.That(result.CellPhoneNumber, Is.EqualTo("1234567890"));
            Assert.That(result.DocumentType, Is.EqualTo("DNI"));
            Assert.That(result.DocumentNumber, Is.EqualTo("12345678"));
        }


        [Test]
        public void GetPersonsByPhoneNumberAsync_ShouldThrowException_WhenServiceReturnsError()
        {
            // Arrange
            _mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Internal Server Error"
                });

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _adapter.GetPersonsByPhoneNumberAsync("1234567890"));
        }
    }
}
