namespace Infrastructure.Soap.Requests
{
    public class GetPersonsByPhoneNumberRequest
    {
        public required string CellPhoneNumber { get; set; }
    }
}
