using System.Xml.Serialization;

namespace Shared.Responses
{
    [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/Contracts.Responses")]
    [XmlRoot("PersonResponse", Namespace = "http://schemas.datacontract.org/2004/07/Contracts.Responses")]
    public class PersonResponse
    {
        [XmlElement("CellPhoneNumber")]
        public string CellPhoneNumber { get; set; }

        [XmlElement("DocumentNumber")]
        public string DocumentNumber { get; set; }

        [XmlElement("DocumentType")]
        public string DocumentType { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
