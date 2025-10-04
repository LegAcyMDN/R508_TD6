using App.Models;
using App.Services.Availability.Interfaces;

namespace App.Services.Availability
{
    public class AvailabilityContext
    {
        private IAvailabilityStrategy _strategy;

        public AvailabilityContext(IAvailabilityStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IAvailabilityStrategy strategy)
        {
            _strategy = strategy;
        }

        public AvailabilityStatus CheckAvailability(Product product, int currentStock)
        {
            return _strategy.CalculateAvailability(product, currentStock);
        }
    }
}
