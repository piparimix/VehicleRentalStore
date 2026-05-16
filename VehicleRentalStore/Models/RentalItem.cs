using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalStore.Models
{
    public abstract class RentalItem
    { 
            public int Id { get; set; }
            public decimal DailyRate { get; set; }
            public decimal HourlyRate { get; set; }
            public ItemStatus Status { get; set; }

        public virtual decimal CalculateRentalCost(TimeSpan rentalDuration)
        {
            int days = rentalDuration.Days;
            int extraHours = rentalDuration.Hours;
            decimal hoursCost = extraHours * HourlyRate;
            if (hoursCost > DailyRate)
            {
                hoursCost = DailyRate;
            }

            return (days * DailyRate) + hoursCost;
        }
    }
}

