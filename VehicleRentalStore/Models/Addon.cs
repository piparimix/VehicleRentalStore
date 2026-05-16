using System;
using VehicleRentalStore.Models.Strategies;
namespace VehicleRentalStore.Models
{
    public class Addon : RentalItem
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public AddonBillingType BillingType { get; set; }
        public decimal FlatFee { get; set; }

        public override decimal CalculateRentalCost(TimeSpan rentalDuration)
        {
            IBillingStrategy strategy = BillingStrategyFactory.GetStrategy(BillingType);
            return strategy.CalculateCost(this, rentalDuration);
        }
    }
}