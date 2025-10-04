using App.Models;
using App.Services.Availability.Interfaces;

namespace App.Services.Availability.Strategies
{
    public class CriticalAvailabilityStrategy : IAvailabilityStrategy
    {
        public AvailabilityStatus CalculateAvailability(Product product, int currentStock)
        {
            if (currentStock < product.MinStock)
            {
                return AvailabilityStatus.Unavailable;
            }

            if (currentStock > product.MaxStock)
            {
                return AvailabilityStatus.Blocked;
            }

            return AvailabilityStatus.Available;
        }
    }
}
