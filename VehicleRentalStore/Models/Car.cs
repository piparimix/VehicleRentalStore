using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }
        public int PassengerCapacity { get; set; }
        public int CargoCapacityLiters { get; set; }
    }
}