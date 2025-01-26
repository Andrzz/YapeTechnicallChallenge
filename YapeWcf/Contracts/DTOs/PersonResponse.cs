using System.Runtime.Serialization;

namespace Contracts.DTOs
{
    [DataContract]
    public class PersonResponse
    {
        [DataMember]
        public string CellPhoneNumber { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string DocumentType { get; set; }

        [DataMember]
        public string DocumentNumber { get; set; }
    }
}
