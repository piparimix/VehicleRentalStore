using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleRentalStore.Data;
using VehicleRentalStore.Models;

namespace VehicleRentalStore.Services
{
    internal class SqlDataService : IDataService
    {
        // All database-related operations would be implemented here, such as connecting to the database, executing queries, and returning results.
        private readonly IDbContextFactory<VehicleRentalDbContext> _contextFactory;
        public SqlDataService(IDbContextFactory<VehicleRentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<T>> GetAllAsync<T>() where T : Vehicle
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            // Fetch all vehicles of the specified type and return them as a list
            return await context.Set<T>().ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<Employee>().FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<Customer>().ToListAsync();
        }
        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<Rental>().ToListAsync();
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<Invoice>().ToListAsync();
        }
        public async Task AddRentalAsync(Rental rental)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Set<Rental>().Add(rental);
            await context.SaveChangesAsync();
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<Location>().ToListAsync();
        }

        public async Task<List<Vehicle>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // TODO: We will need to add logic here later to filter out vehicles 
            // that are already booked during these dates. 
            // For now, this returns all vehicles to clear the compiler error.
            return await context.Set<Vehicle>().ToListAsync();
        }
        public async Task<List<InsurancePlan>> GetAllInsurancePlansAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<InsurancePlan>().ToListAsync();
        }
    }
}
