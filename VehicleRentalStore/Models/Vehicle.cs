using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Vehicle : RentalItem
    {
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int ManufactureYear { get; set; }
        public required string LicensePlate { get; set; }
        public required string VIN { get; set; }
        public required FuelType FuelType { get; set; }
        public decimal RefuelingPremiumPerUnit { get; set; }

        // Physical Accuracy
        public double FuelTankCapacityLiters { get; set; }
        public double BatteryCapacityKWh { get; set; } // For EVs
        public TireType CurrentTires { get; set; }

        // Tracking & Maintenance
        public int CurrentOdometerKm { get; set; }
        public int NextMaintenanceOdometerKm { get; set; }
        public DateTime? NextInspectionDate { get; set; }
        public TransmissionType Transmission { get; set; }
        public required string PrimaryColor { get; set; }
        public required string Description { get; set; }
        public int? IncludedKilometersPerDay { get; set; }
        public decimal ExtraKilometerRate { get; set; }
        public string FullName() => $"{Brand} {Model} ({ManufactureYear})".Trim();
        public string VehicleType => this is Car ? "Car" : (this is Motorcycle ? "Motorcycle" : "Other");

        private readonly List<MaintenanceLog> _maintenanceHistory = new();
        public IReadOnlyCollection<MaintenanceLog> MaintenanceHistory => _maintenanceHistory.AsReadOnly();

        private readonly List<ConditionLog> _damageHistory = new();
        public IReadOnlyCollection<ConditionLog> DamageHistory => _damageHistory.AsReadOnly();
    }
}
