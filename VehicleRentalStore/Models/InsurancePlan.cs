using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class InsurancePlan
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal DailyCost { get; set; }
        public decimal Deductible { get; set; }
        public string? Description { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
