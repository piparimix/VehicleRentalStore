using System;

namespace VehicleRentalStore.Models.Strategies
{
    // 1. The Strategy Interface
    public interface IBillingStrategy
    {
        decimal CalculateCost(Addon addon, TimeSpan rentalDuration);
    }

    // 2. Concrete Strategies
    public class PerRentalBillingStrategy : IBillingStrategy
    {
        public decimal CalculateCost(Addon addon, TimeSpan rentalDuration)
            => addon.FlatFee;
    }

    public class PerDayBillingStrategy : IBillingStrategy
    {
        public decimal CalculateCost(Addon addon, TimeSpan rentalDuration)
            => addon.DailyRate * rentalDuration.Days;
    }

    public class PerWeekBillingStrategy : IBillingStrategy
    {
        public decimal CalculateCost(Addon addon, TimeSpan rentalDuration)
            => addon.DailyRate * (decimal)Math.Ceiling(rentalDuration.Days / 7.0);
    }

    // 3. A Factory to bridge the EF Core Enum to the Strategy
    public static class BillingStrategyFactory
    {
        public static IBillingStrategy GetStrategy(AddonBillingType billingType)
        {
            return billingType switch
            {
                AddonBillingType.PerRental => new PerRentalBillingStrategy(),
                AddonBillingType.PerDay => new PerDayBillingStrategy(),
                AddonBillingType.PerWeek => new PerWeekBillingStrategy(),
                _ => throw new NotSupportedException($"Billing type {billingType} is not supported.")
            };
        }
    }
}