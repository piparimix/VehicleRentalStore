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
    }
}
