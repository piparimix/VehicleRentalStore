using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
        public enum EmployeeRole
        {
            Manager,
            Staff,
            Maintenance
        }

        public enum RentalStatus
        {
            Active,
            Completed,
            Cancelled
        }

        public enum InvoiceStatus
        {
            Unpaid,
            Paid,
            Overdue
        }
        public enum  CustomerType
        {
            Organization,
            PrivateIndividual,
            Company
        }

        public enum FuelType
        {
            Gasoline,
            Diesel,
            Electric,
            Hybrid,
            None
        }
        public enum DamageType
        {
            Scratch,
            Dent,
            GlassCrack,
            InteriorStain,
            InteriorTear,
            MechanicalIssue,
            MissingPart,
            Other
        }

        public enum DamageSeverity
        {
            Cosmetic,   // Doesn't affect driving (e.g., small scratch)
            Moderate,   // Needs fixing soon, but drivable (e.g., cracked taillight)
            Severe      // Grounded vehicle, cannot be rented (e.g., broken windshield)
        }

    public enum TireType
    {
        AllSeason,
        Summer,
        Winter,
        Performance,
        OffRoad
    }

    public enum  AddonBillingType
    {
        PerRental,
        PerDay,
        PerWeek
    }

    public enum  ItemStatus
    {
        Available,
        OnRent,
        UnderMaintenance,
        Decommissioned
    }
    public enum TransmissionType
    {
        Manual,
        Automatic
    }
    public enum FuelPolicy
    {
        FullToFull,     // Must return at 100%, otherwise massive premium penalty.
        PrePurchase,    // Paid upfront, can return at 0%. No refunds for unused fuel.
        SameToSame      // E.g., Take it at 50%, return it at 50%.
    }
}
