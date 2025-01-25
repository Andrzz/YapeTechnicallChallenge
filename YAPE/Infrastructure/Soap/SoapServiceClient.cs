using SoapHttpClient.Enums;
using SoapHttpClient;
using System.Xml.Serialization;
using Infrastructure.Soap.Requests;
using Infrastructure.Soap.Responses;

namespace Infrastructure.Soap
{
    public class SoapServiceClient
    {
        private readonly string _soapUrl;

        public SoapServiceClient(string soapUrl)
        {
            _soapUrl = soapUrl;
        }

        public async Task<GetPersonsByPhoneNumberResponse> GetPersonsByPhoneNumberAsync(string cellPhoneNumber)
        {
            // Crear el cliente SOAP
            var soapClient = new SoapClient();

            // Crear el request
            var request = new GetPersonsByPhoneNumberRequest
            {
                CellPhoneNumber = cellPhoneNumber
            };

            // Realizar la llamada al método SOAP
            var response = await soapClient.PostAsync(
                new Uri(_soapUrl),
                SoapVersion.Soap11,
                request, // Objeto request
                "http://tempuri.org/IPersonService/GetPersonsByPhoneNumber" // Acción SOAP
            );

            // Procesar la respuesta SOAP
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserializar el contenido XML a un objeto
                var serializer = new XmlSerializer(typeof(GetPersonsByPhoneNumberResponse));
                using (var stringReader = new StringReader(responseContent))
                {
                    var result = (GetPersonsByPhoneNumberResponse)serializer.Deserialize(stringReader);
                    return result;
                }
            }
            else
            {
                throw new Exception("Error en la llamada al servicio SOAP: " + response.ReasonPhrase);
            }
        }
    }
}
