using Contracts.DTOs;
using System.ServiceModel;

namespace Contracts.Interfaces
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        PersonResponse GetPersonsByPhoneNumber(string cellPhoneNumber);
    }
}
