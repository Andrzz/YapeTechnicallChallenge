using Contracts.Responses;
using System.Collections.Generic;
using System.ServiceModel;

namespace PersonService.Contracts
{

    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        List<PersonResponse> GetPersonsByPhoneNumber(string cellPhoneNumber);
    }    
}
