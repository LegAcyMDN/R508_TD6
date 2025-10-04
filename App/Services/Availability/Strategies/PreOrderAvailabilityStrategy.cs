using App.Models;
using App.Services.Availability.Interfaces;

namespace App.Services.Availability.Strategies
{
    public class PreOrderAvailabilityStrategy : IAvailabilityStrategy
    {
        public AvailabilityStatus CalculateAvailability(Product product, int currentStock)
        {
            if (currentStock < product.MinStock)
            {
                return AvailabilityStatus.PreOrder;
            }

            // Pas de restriction sur le surstock
            return AvailabilityStatus.Available;
        }
    }
}
