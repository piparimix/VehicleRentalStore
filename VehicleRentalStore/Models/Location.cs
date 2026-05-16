using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public class Location
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<Rental> Pickups { get; set; } = new List<Rental>();
        public ICollection<Rental> Dropoffs { get; set; } = new List<Rental>();
    }
}
