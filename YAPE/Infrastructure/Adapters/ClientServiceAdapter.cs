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
            if (string.IsNullOrWhiteSpace(cellPhoneNumber))
            {
                throw new ArgumentException("CellPhoneNumber cannot be null or empty.", nameof(cellPhoneNumber));
            }

            try
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
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var soapFaultMessage = SOAPFaultSerializer.ExtractSoapFaultMessage(responseContent);
                    throw new Exception($"SOAP Service Error: {soapFaultMessage} (Status Code: {httpResponse.StatusCode})");
                }

                var responseContentSuccess = await httpResponse.Content.ReadAsStringAsync();
                return SOAPSuccessfullSerializer.DeserializeSoapResponse(responseContentSuccess);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Network error occurred while calling the SOAP service.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred in GetPersonsByPhoneNumberAsync: {ex.Message}", ex);
            }
        }



    }
}
