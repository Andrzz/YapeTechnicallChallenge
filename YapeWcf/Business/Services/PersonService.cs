using Contracts.DTOs;
using Contracts.Interfaces;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PersonResponse GetPersonsByPhoneNumber(string cellPhoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cellPhoneNumber))
                {
                    throw new ArgumentException("The phone number cannot be null or empty.", nameof(cellPhoneNumber));
                }

                if (cellPhoneNumber.Length != 10 || !long.TryParse(cellPhoneNumber, out _))
                {
                    throw new ArgumentException("The phone number must be a valid 10-digit number.", nameof(cellPhoneNumber));
                }
                var person = _repository.GetByPhoneNumber(cellPhoneNumber);
                if (person == null)
                {
                    throw new KeyNotFoundException("No person found with the given phone number.");
                }
                return person;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw new Exception("An error occurred while trying to get the person by phone number.", ex);

            }
        }
        public string HealthCheck()
        {
            return "Service is running.";
        }
        #region Private Methods
        private void LogError(Exception ex)
        {
            System.Diagnostics.Trace.TraceError($"[{DateTime.Now}] Error: {ex.Message}\n{ex.StackTrace}");
            System.Diagnostics.Trace.Flush();
        }
        #endregion
    }
}
