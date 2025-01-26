# YAPE Technical Challenge

## Project Description
This project consists of building a technical solution with two main components:

1. **WCF Service**: Developed in **.NET Framework 4.8**, it exposes a SOAP-based method to retrieve person details by their phone number. This service adheres to best practices, including:
   - Layered architecture.
   - Dependency injection.
   - Robust exception handling.
   - JSON-based data storage for person records.

2. **RESTful API**: Built with **.NET 8**, it allows users to:
   - Register clients using data validated via the WCF service.
   - Perform health checks.
   - Retrieve validated client data from an in-memory database.

The solution emphasizes modularity, scalability, and maintainability, leveraging modern software development patterns and tools.

---

## Key Features

### WCF Service
- **SOAP method**: `GetPersonsByPhoneNumber(string cellPhoneNumber)` for retrieving person details.
- **Exception Handling**: Logs errors to a file using `System.Diagnostics`.
- **JSON Data Storage**: Stores person records in a simple JSON file.

### RESTful API
- **Endpoints**:
  - `POST /api/Client/CreateClient`: Register new clients.
  - `GET /api/Client/GetValidatedClients`: Retrieve validated clients.
  - `GET /api/Client/HealthCheck`: Perform service health checks.
- **Simulated Database**: Stores validated clients in an in-memory database using `EntityFrameworkCore`.
- **Error Logging**: Uses `Serilog` for comprehensive error tracking.

### Architecture
- **Layered Structure**: Separation of concerns with distinct projects for business logic, infrastructure, and domain.
- **Hexagonal Architecture**: Ports and adapters pattern for maintainability and extensibility.
- **Dependency Injection**: Decouples services using DI containers.
- **Design Patterns**: Includes Repository, Adapter, and Logging Middleware patterns.

---

## Prerequisites

### Software Requirements
- **Mandatory**: Visual Studio 2022 (latest updates)
- **Optional**: SOAPUI or Postman for testing.

---

## Technologies and Dependencies

### WCF
- **Framework**: .NET Framework 4.8
- **NuGet Packages**:
  - `Autofac` v8.0.0
  - `Autofac.Wcf` v7.0.0
  - `Microsoft.NET.Test.Sdk` v17.12
  - `Moq` v4.20.72
  - `Newtonsoft.Json` v13.0.3
  - `NUnit` v4.3.2
  - `NUnit3TestAdapter` v4.6.0

### API
- **Framework**: .NET 8
- **NuGet Packages**:
  - `Serilog.Sinks.File` v6.0.0
  - `Microsoft.EntityFrameworkCore` v9.0.1
  - `Microsoft.EntityFrameworkCore.InMemory` v9.0.1
  - `Serilog.AspNetCore` v9.0.0
  - `Swashbuckle.AspNetCore` v6.6.2
  - `System.ServiceModel.Http` v8.1.1

---

## Setup and Execution

### Configuration
1. Add the WCF service URL in the `appsettings.json` file of the API:
   ```json
   {
     "SoapSettings": {
       "Url": "http://localhost:XXXXX/PersonWCFService.svc"
     }
   }

### Execution
 - Open the solution in Visual Studio 2022.
 - Set the WCF and API as startup projects.
 - Run both services using IIS in Visual Studio.

## Architecture
### Project Structure
**WCF**
 - PersonWebService: WCF service entry point.
 - Business: Contains service logic and business rules.
 - Contracts: Defines data transfer objects (DTOs) and interfaces.
 - DataAccess: Manages the JSON-based data source.
 - UnitTests: Contains unit tests for the WCF components.

**API**
 - API: RESTful API entry point.
 - Application: Contains business logic for the API.
 - Domain: Defines core entities such as Client and YapeValidatedClient.
 - Infrastructure: Handles SOAP integration, in-memory database, and logging.
 - Shared: Contains shared components like DTOs and serializers.
 - UnitTests: Contains unit tests for the API components.

## Design Patterns
 - Hexagonal Architecture
 - Dependency Injection
 - Repository Pattern
 - Adapter Pattern
 - Logging Middleware

## Unit Testing
Framework: NUnit
### Execution:
 - Run tests from the Test Explorer in Visual Studio.
 - Right-click on any test class and select Run Tests.
 - Coverage: Includes tests for the WCF service, API business logic, and adapters.

## Endpoints
### API 
**POST /api/Client/CreateClient**
**Request:**
```json
{
  "name": "jhon",
  "lastName": "doe",
  "cellPhoneNumber": "1234567890",
  "documentType": "DNI",
  "documentNumber": "12345678",
  "reasonOfUse": "Traveling"
}
```
**Response:**
```json
{
  "message": "User created successfully",
  "clientId": "c33ea802-c32c-4040-ab69-18356fff54fc"
}
```
**GET /api/Client/GetValidatedClients**
**Response:**
```json
[
  {
    "id": "c33ea802-c32c-4040-ab69-18356fff54fc",
    "name": "John",
    "lastName": "Doe",
    "cellPhoneNumber": "1234567890",
    "documentType": "DNI",
    "documentNumber": "12345678",
    "reasonOfUse": "Traveling"
  }
]
```
**GET /api/Client/HealthCheck**
**Response:**
 - plaintext: Service Running...


## WCF
**SOAP Method:**
 - GetPersonsByPhoneNumber(string cellPhoneNumber) -> Retrieves person details by phone number.

## License
 - This project does not include a license.

## Author
 - Andres Felipe Osorio Echavarria

## Additional Notes
When starting the WCF service in Visual Studio, you may encounter a warning:
The target assembly contains no service types. You may need to adjust the code access security policy of this assembly.
Simply click OK to proceed.

Make sure both the WCF and API are running on the same machine during testing.
