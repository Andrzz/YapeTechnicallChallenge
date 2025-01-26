YAPE Technical Challenge

Project Description
This project consists of building a technical solution with two main components:

A WCF Service developed in .NET Framework 4.8, which exposes a SOAP-based method to retrieve person details by their phone number. This service adheres to best practices, including a layered architecture, dependency injection, and robust exception handling.
A RESTful API built with .NET 8, which allows users to register clients using data validated via the WCF service. The API uses Hexagonal Architecture and incorporates a simulated in-memory database.
The solution emphasizes modularity, scalability, and maintainability, leveraging modern software development patterns and tools.

Key Features

WCF Service:

SOAP method: GetPersonsByPhoneNumber(string cellPhoneNumber) for retrieving person details.
JSON-based data storage for person records.
Robust exception handling and logging mechanisms.

RESTful API:

POST endpoint for creating clients validated against the WCF service.
GET endpoints for retrieving validated clients and performing health checks.
Simulated in-memory database for client storage.
Comprehensive error handling and logging using Serilog.

Architecture:

Layered structure with separation of concerns.
Hexagonal Architecture.
Dependency Injection and Repository Pattern.

Prerequisites
Software Requirements
Mandatory:
Visual Studio 2022 (latest updates)
Optional:
SOAPUI or Postman for testing.

Technologies and Versions
WCF: .NET Framework 4.8
API: .NET 8

Dependencies
WCF
Autofac v8.0.0
Autofac.Wcf v7.0.0
Microsoft.NET.Test.Sdk v17.12
Moq v4.20.72
Newtonsoft.Json v13.0.3
NUnit v4.3.2
NUnit3TestAdapter v4.6.0

API
Serilog.Sinks.File v6.0.0
Microsoft.EntityFrameworkCore v9.0.1
Microsoft.EntityFrameworkCore.InMemory v9.0.1
Serilog.AspNetCore v9.0.0
Swashbuckle.AspNetCore v6.6.2
System.ServiceModel.Http v8.1.1

Setup and Execution
Configuration
WCF: No additional configuration is needed.
API: Add the WCF service URL in the appsettings.json file:
json
{
  "SoapSettings": {
    "Url": "http://localhost:XXXXX/PersonWCFService.svc"
  }
}

Execution
Open the solution in Visual Studio 2022.
Set the WCF and API as startup projects.
Run both services using IIS in Visual Studio.

Architecture
Project Structure
WCF
PersonWebService: WCF service entry point.
Business: Contains service logic and business rules.
Contracts: Defines data transfer objects (DTOs) and interfaces.
DataAccess: Manages the JSON-based data source.
UnitTests: Contains unit tests for the WCF components.

API
API: RESTful API entry point.
Application: Contains business logic for the API.
Domain: Defines core entities such as Client and YapeValidatedClient.
Infrastructure: Handles SOAP integration, in-memory database, and logging.
Shared: Contains shared components like DTOs and serializers.
UnitTests: Contains unit tests for the API components.

Design Patterns
Hexagonal Architecture
Dependency Injection
Repository Pattern
Adapter Pattern
Logging Middleware

Unit Testing
Framework: NUnit
Execution:
Run tests from the Test Explorer in Visual Studio.
Right-click on any test class and select Run Tests.
Coverage: Includes tests for the WCF service, API business logic, and adapters.

Endpoints
API
POST /api/Client/CreateClient

Request:
json
{
  "name": "jhon",
  "lastName": "doe",
  "cellPhoneNumber": "1234567890",
  "documentType": "DNI",
  "documentNumber": "12345678",
  "reasonOfUse": "Traveling"
}
Response:
json
{
  "message": "User created successfully",
  "clientId": "c33ea802-c32c-4040-ab69-18356fff54fc"
}
GET /api/Client/GetValidatedClients

Response:
json
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

GET /api/Client/HealthCheck

Response:
plaintext
Service Running...

WCF
SOAP Method:
GetPersonsByPhoneNumber(string cellPhoneNumber)
Retrieves person details by phone number.
License
This project does not include a license.

Author
Andres Felipe Osorio Echavarria
Additional Notes
When starting the WCF service in Visual Studio, you may encounter a warning:

vbnet
The target assembly contains no service types. You may need to adjust the code access security policy of this assembly.
Simply click OK to proceed.

Make sure both the WCF and API are running on the same machine during testing.