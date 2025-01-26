using System.Text;
using Shared.Interfaces;
using Shared.RequestBuilders;
using Shared.Responses;
using Shared.Serializers;

namespace Infrastructure.Adapters
{
    public class ClientServiceAdapter : IPersonServiceClient
    {
        private readonly string _soapUrl;
        private readonly HttpClient _httpClient;

        public ClientServiceAdapter(string soapUrl)
        {
            _soapUrl = soapUrl ?? throw new ArgumentNullException(nameof(soapUrl));
            _httpClient = new HttpClient();
        }

        public async Task<GetPersonsByPhoneNumberResult> GetPersonsByPhoneNumberAsync(string cellPhoneNumber)
        {
            var soapRequestBody = SOAPBuilder.BuildSoapRequest(cellPhoneNumber);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _soapUrl)
            {
                Content = new StringContent(soapRequestBody, Encoding.UTF8, "text/xml")
            };
            httpRequest.Headers.Add("SOAPAction", "http://tempuri.org/IPersonService/GetPersonsByPhoneNumber");
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Error calling SOAP service: {httpResponse.ReasonPhrase}");
            }
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return SOAPSerializer.DeserializeSoapResponse(responseContent);
        }
       
    }
}
