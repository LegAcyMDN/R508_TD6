using App.Models;
using App.Repositories.Interfaces;
using App.Services.Notifications.Interfaces;

namespace App.Services.Notifications.Channels
{
    public class EmailNotificationChannel : INotificationChannel
    {
        private readonly IEmailService _emailService;

        public EmailNotificationChannel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task NotifyAsync(Product product)
        {
            await _emailService.SendAsync(
                subject: $"Produit disponible: {product.ProductName}",
                body: $"Le produit '{product.ProductName}' est maintenant disponible à l'achat."
            );
        }

        public string GetChannelName() => "Email";
    }
}
