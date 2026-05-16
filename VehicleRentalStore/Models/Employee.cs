using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Employee : Person
    {
        public EmployeeRole Role { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    }
}
