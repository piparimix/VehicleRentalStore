using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class MaintenanceLog
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public required Vehicle Vehicle { get; set; }
        public required Employee Employee { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
