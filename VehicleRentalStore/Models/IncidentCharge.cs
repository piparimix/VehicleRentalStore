using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class IncidentCharge
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public required Rental Rental { get; set; }

        public DateTime OffenseDate { get; set; }
        public required string Description { get; set; } // e.g., "Speeding Camera - Route 6"
        public decimal FineAmount { get; set; }
        public decimal AdminFee { get; set; } // Rental companies charge ~40€ just to process the ticket
        public bool IsBilledToCustomer { get; set; }
    }
}
