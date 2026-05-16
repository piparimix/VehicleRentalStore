using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class ConditionLog
    {
        public int Id { get; set; }

        // Foreign Keys & Navigation Properties
        public int VehicleId { get; set; }
        public required Vehicle Vehicle { get; set; }

        public int EmployeeId { get; set; }
        public required Employee Employee { get; set; }

        // Nullable because damage might be discovered in the parking lot 
        // while the vehicle is NOT actively rented out.
        public int? RentalId { get; set; }
        public Rental? Rental { get; set; }

        // Core Data
        public DateTime DateReported { get; set; } = DateTime.UtcNow;
        public DamageType Type { get; set; }
        public DamageSeverity Severity { get; set; }

        public required string LocationOnVehicle { get; set; } // e.g., "Rear Left Door", "Windshield"
        public required string Description { get; set; }

        // Resolution Tracking
        public bool IsRepaired { get; set; } = false;
        public decimal? EstimatedRepairCost { get; set; }
    }
}
