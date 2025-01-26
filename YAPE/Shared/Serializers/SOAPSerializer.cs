using Shared.Responses;
using System.Xml.Serialization;
using System.Xml;

namespace Shared.Serializers
{
    public static class SOAPSerializer
    {
        public static GetPersonsByPhoneNumberResult DeserializeSoapResponse(string soapResponse)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapResponse);
            var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaceManager.AddNamespace("tempuri", "http://tempuri.org/");
            namespaceManager.AddNamespace("a", "http://schemas.datacontract.org/2004/07/Contracts.DTOs");
            var responseNode = xmlDocument.SelectSingleNode("//tempuri:GetPersonsByPhoneNumberResult", namespaceManager);

            if (responseNode == null)
            {
                throw new Exception("Could not find the GetPersonsByPhoneNumberResult node inside the SOAP response.");
            }
            var serializer = new XmlSerializer(typeof(GetPersonsByPhoneNumberResult), "http://tempuri.org/");
            using (var nodeReader = new XmlNodeReader(responseNode))
            {
                try
                {
                    return (GetPersonsByPhoneNumberResult)serializer.Deserialize(nodeReader);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deserializing the SOAP response.", ex);
                }
            }
        }

    }
}
