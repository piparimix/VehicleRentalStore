using System;

namespace VehicleRentalStore.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}