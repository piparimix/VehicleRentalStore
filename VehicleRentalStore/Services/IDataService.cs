using System;
using System.Collections.Generic;
using System.Text;
using VehicleRentalStore.Models;


namespace VehicleRentalStore.Services
{
    // This interface defines the contract for any data service implementation.
    // It allows you to abstract away the details of how data is retrieved and manipulated,
    // making it easier to switch out implementations (e.g., from a SQL database to an API) without affecting the rest of your application.
    public interface IDataService
    {
        Task<List<T>> GetAllAsync<T>() where T : Vehicle;
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Rental>> GetAllRentalsAsync();
        Task<List<Invoice>> GetAllInvoicesAsync();
    }
}
