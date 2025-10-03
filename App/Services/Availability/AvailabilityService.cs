using App.Models;
using App.Repositories.Interfaces;
using App.Services.Availability.Interfaces;
using App.Services.Availability.Strategies;
using App.Services.Notifications.Interfaces;

namespace App.Services.Availability
{
    public class AvailabilityService
    {
        private readonly IAvailabilityHistoryRepository _historyRepository;
        private readonly INotificationService _notificationService;
        private readonly Dictionary<string, IAvailabilityStrategy> _strategies;

        public AvailabilityService(
            IAvailabilityHistoryRepository historyRepository,
            INotificationService notificationService)
        {
            _historyRepository = historyRepository;
            _notificationService = notificationService;

            // Initialisation des stratégies disponibles
            _strategies = new Dictionary<string, IAvailabilityStrategy>
            {
                ["Strict"] = new StrictAvailabilityStrategy(),
                ["PreOrder"] = new PreOrderAvailabilityStrategy(),
                ["Critical"] = new CriticalAvailabilityStrategy()
            };
        }

        public async Task<AvailabilityStatus> UpdateProductAvailabilityAsync(
            Product product,
            int newStock,
            string strategyName = "Strict")
        {
            // Sélection de la stratégie
            if (!_strategies.TryGetValue(strategyName, out var strategy))
            {
                throw new ArgumentException($"Strategy '{strategyName}' not found");
            }

            var context = new AvailabilityContext(strategy);

            // Récupération du dernier statut
            var lastHistory = await _historyRepository.GetLatestForProductAsync(product.IdProduct);
            var previousStatus = lastHistory?.Status;

            // Calcul du nouveau statut
            var newStatus = context.CheckAvailability(product, newStock);

            // Création de l'entrée d'historique
            var historyEntry = new AvailabilityHistory
            {
                ProductId = product.IdProduct,
                Timestamp = DateTime.UtcNow,
                Status = newStatus,
                StrategyUsed = strategyName,
                StockAtTime = newStock
            };

            await _historyRepository.AddAsync(historyEntry);

            // Notification si le produit devient disponible
            if (previousStatus != AvailabilityStatus.Available &&
                newStatus == AvailabilityStatus.Available)
            {
                await _notificationService.NotifyProductAvailableAsync(product);
            }

            return newStatus;
        }

        public IAvailabilityStrategy GetStrategy(string strategyName)
        {
            return _strategies.TryGetValue(strategyName, out var strategy)
                ? strategy
                : throw new ArgumentException($"Strategy '{strategyName}' not found");
        }

        public IEnumerable<string> GetAvailableStrategies()
        {
            return _strategies.Keys;
        }
    }
}
