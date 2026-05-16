using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Customer : Person
    {
        public CustomerType Type { get; set; } = CustomerType.PrivateIndividual;
        public required List<string> LicenseCategories { get; set; }
        public required string DriversLicenseNumber { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsAnonymized { get; set; } = false;
        public string Ssn { get; set; } = string.Empty; // Social Security Number for private individuals, Tax ID for businesses
        private readonly List<Rental> _rentals = new();
        public IReadOnlyCollection<Rental> Rentals => _rentals.AsReadOnly();

        private readonly List<IncidentCharge> _trafficViolations = new();
        public IReadOnlyCollection<IncidentCharge> TrafficViolations => _trafficViolations.AsReadOnly();
        public void AddRental(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            // You can add validation logic here (e.g., check if customer is banned before adding)
            _rentals.Add(rental);
        }

        public void AddTrafficViolation(IncidentCharge charge)
        {
            ArgumentNullException.ThrowIfNull(charge);
            _trafficViolations.Add(charge);
        }
    }
}
