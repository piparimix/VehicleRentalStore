using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public required Customer Customer { get; set; }

        private readonly List<RentalItem> _rentedItems = new();
        public IReadOnlyCollection<RentalItem> RentedItems => _rentedItems.AsReadOnly();
        public required Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Active;
        public decimal TotalAmount { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public int StartOdometerKm { get; set; }
        public int? EndOdometerKm { get; set; }
        public int StartFuelPercentage { get; set; }
        public int? EndFuelPercentage { get; set; }
        public int PickupLocationId { get; set; }
        public Location? PickupLocation { get; set; } 
        public int DropoffLocationId { get; set; }
        public Location? DropoffLocation { get; set; }
        public int? InsurancePlanId { get; set; }
        public InsurancePlan? InsurancePlan { get; set; }
        public decimal SecurityDepositAmount { get; set; }
        public bool IsSecurityDepositReleased { get; set; }

        private readonly List<IncidentCharge> _incidentCharges = new();
        public IReadOnlyCollection<IncidentCharge> IncidentCharges => _incidentCharges.AsReadOnly();
        private readonly List<ConditionLog> _inspectionLogs = new();
        public IReadOnlyCollection<ConditionLog> InspectionLogs => _inspectionLogs.AsReadOnly();
        private readonly List<Customer> _additionalDrivers = new();
        public IReadOnlyCollection<Customer> AdditionalDrivers => _additionalDrivers.AsReadOnly();
        public FuelPolicy FuelPolicy { get; set; } = FuelPolicy.FullToFull;
    }
}
