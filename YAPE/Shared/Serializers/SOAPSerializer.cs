using Shared.Responses;
using System.Xml.Serialization;
using System.Xml;

namespace Shared.Serializers
{
    public static class SOAPSerializer
    {
        public static PersonResponse DeserializeSoapResponse(string soapResponse)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapResponse);

            var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaceManager.AddNamespace("tempuri", "http://tempuri.org/");
            namespaceManager.AddNamespace("a", "http://schemas.datacontract.org/2004/07/Contracts.Responses");
            var responseNode = xmlDocument.SelectSingleNode("//a:PersonResponse", namespaceManager);

            if (responseNode == null)
            {
                throw new Exception("No se pudo encontrar el nodo PersonResponse en la respuesta SOAP.");
            }
            var serializer = new XmlSerializer(typeof(PersonResponse), "http://schemas.datacontract.org/2004/07/Contracts.Responses");
            using (var nodeReader = new XmlNodeReader(responseNode))
            {
                return (PersonResponse)serializer.Deserialize(nodeReader);
            }
        }
    }
}
