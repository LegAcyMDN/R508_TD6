using App.Services.Availability.Interfaces;

namespace App.Models
{
    public class AvailabilityHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime Timestamp { get; set; }
        public AvailabilityStatus Status { get; set; }
        public string StrategyUsed { get; set; }
        public int StockAtTime { get; set; }
    }
}
