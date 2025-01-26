using System.Xml;

namespace Shared.Serializers
{
    public static class SOAPFaultSerializer
    {
        public static string ExtractSoapFaultMessage(string soapResponse)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(soapResponse);

                var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                namespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaceManager.AddNamespace("a", "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher");
                namespaceManager.AddNamespace("detail", "http://schemas.datacontract.org/2004/07/System.ServiceModel");

                // Encontrar el nodo Fault
                var faultNode = xmlDocument.SelectSingleNode("//s:Fault", namespaceManager);
                if (faultNode != null)
                {
                    var innerExceptionMessageNode = faultNode.SelectSingleNode("//detail:InnerException/detail:Message", namespaceManager);
                    if (innerExceptionMessageNode != null)
                    {
                        return innerExceptionMessageNode.InnerText.Trim();
                    }
                    var faultStringNode = faultNode.SelectSingleNode("faultstring");
                    if (faultStringNode != null)
                    {
                        return faultStringNode.InnerText.Trim();
                    }
                }

                return "The SOAP response does not contain a valid Fault node.";
            }
            catch (Exception ex)
            {
                return $"Error parsing SOAP fault: {ex.Message}";
            }
        }
    }
}
