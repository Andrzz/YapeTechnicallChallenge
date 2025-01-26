using System.Xml.Serialization;

namespace Shared.Responses
{
    [XmlType(Namespace = "http://tempuri.org/")]
    [XmlRoot("GetPersonsByPhoneNumberResult", Namespace = "http://tempuri.org/")]
    public class GetPersonsByPhoneNumberResult
    {
        [XmlElement("CellPhoneNumber", Namespace = "http://schemas.datacontract.org/2004/07/Contracts.DTOs")]
        public string CellPhoneNumber { get; set; }

        [XmlElement("DocumentNumber", Namespace = "http://schemas.datacontract.org/2004/07/Contracts.DTOs")]
        public string DocumentNumber { get; set; }

        [XmlElement("DocumentType", Namespace = "http://schemas.datacontract.org/2004/07/Contracts.DTOs")]
        public string DocumentType { get; set; }

        [XmlElement("LastName", Namespace = "http://schemas.datacontract.org/2004/07/Contracts.DTOs")]
        public string LastName { get; set; }

        [XmlElement("Name", Namespace = "http://schemas.datacontract.org/2004/07/Contracts.DTOs")]
        public string Name { get; set; }
    }
}
