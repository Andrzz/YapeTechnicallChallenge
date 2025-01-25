namespace Shared.RequestBuilders
{
    public static class SOAPBuilder
    {
        public static string BuildSoapRequest(string cellPhoneNumber)
        {
            return $@"
            <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                <s:Body>
                    <GetPersonsByPhoneNumber xmlns=""http://tempuri.org/"">
                        <cellPhoneNumber>{cellPhoneNumber}</cellPhoneNumber>
                    </GetPersonsByPhoneNumber>
                </s:Body>
            </s:Envelope>";
        }
    }
}
