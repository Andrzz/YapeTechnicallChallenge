using System.Xml.Serialization;

namespace Infrastructure.Soap.Responses
{
    [XmlRoot(ElementName = "GetPersonsByPhoneNumberResponse", Namespace = "http://tempuri.org/")]
    public class GetPersonsByPhoneNumberResponse
    {
        public string? CellPhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
    }
}
