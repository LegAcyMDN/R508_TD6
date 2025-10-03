using App.Models;
using App.Services.Notifications.Interfaces;

namespace App.Services.Notifications.Channels
{
    public class ConsoleNotificationChannel : INotificationChannel
    {
        public Task NotifyAsync(Product product)
        {
            Console.WriteLine($"[CONSOLE] Produit '{product.ProductName}' est maintenant disponible !");
            return Task.CompletedTask;
        }

        public string GetChannelName() => "Console";
    }
}
