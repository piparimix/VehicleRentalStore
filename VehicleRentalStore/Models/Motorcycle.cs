using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Motorcycle : Vehicle
    {
        public int EngineCapacityCc { get; set; }
        public bool RequiresSpecialLicense { get; set; }
    }
}
