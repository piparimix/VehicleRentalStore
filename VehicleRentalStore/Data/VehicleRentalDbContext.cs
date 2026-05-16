using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VehicleRentalStore.Models;

namespace VehicleRentalStore.Data
{
    public class VehicleRentalDbContext : DbContext
    {
        // 1. Persons
        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // 2. Fleet & Inventory
        public DbSet<RentalItem> RentalItems { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Addon> Addons { get; set; }

        // 3. Operations & Financials
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ConditionLog> ConditionLogs { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<IncidentCharge> IncidentCharges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite is perfect for local development and WPF prototyping.
            optionsBuilder.UseSqlite("Data Source=VehicleRental.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -- A. Configure Person Inheritance (TPH) --
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("PersonType")
                .HasValue<Customer>("Customer")
                .HasValue<Employee>("Employee");

            // -- B. Configure RentalItem Inheritance (TPH) --
            modelBuilder.Entity<RentalItem>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<Car>("Car")
                .HasValue<Motorcycle>("Motorcycle")
                .HasValue<Addon>("Addon");

            // -- C. Location Relationships (Prevent Cascade Delete loops) --
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.PickupLocation)
                .WithMany(l => l.Pickups)
                .HasForeignKey(r => r.PickupLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.DropoffLocation)
                .WithMany(l => l.Dropoffs)
                .HasForeignKey(r => r.DropoffLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // -- D. Map the Many-to-Many Relationships --
            // EF Core 5+ handles Many-to-Many automatically, but explicitly defining it
            // is best practice for collections like Additional Drivers.
            modelBuilder.Entity<Rental>()
                .HasMany(r => r.RentedItems)
                .WithMany();

            modelBuilder.Entity<Rental>()
                .HasMany(r => r.AdditionalDrivers)
                .WithMany();

            // -- E. Resolve Customer <-> Rental Ambiguity --
            // This tells EF Core that Customer.Rentals maps to the primary payer, NOT the additional drivers.
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}