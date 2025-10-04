using App.Models;
using App.Services.Availability.Interfaces;

namespace App.Services.Availability.Strategies;

public class StrictAvailabilityStrategy : IAvailabilityStrategy
{
    public AvailabilityStatus CalculateAvailability(Product product, int currentStock)
    {
        if (currentStock < product.MinStock || currentStock > product.MaxStock)
        {
            return AvailabilityStatus.Unavailable;
        }
        return AvailabilityStatus.Available;
    }
}
