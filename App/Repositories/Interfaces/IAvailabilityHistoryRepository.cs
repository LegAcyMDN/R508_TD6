using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IAvailabilityHistoryRepository
    {
        Task<AvailabilityHistory> GetLatestForProductAsync(int productId);
        Task<IEnumerable<AvailabilityHistory>> GetHistoryForProductAsync(int productId);
        Task AddAsync(AvailabilityHistory history);
    }
}
