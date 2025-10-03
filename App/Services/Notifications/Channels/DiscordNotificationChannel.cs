using App.Models;
using App.Repositories.Interfaces;
using App.Services.Notifications.Interfaces;

namespace App.Services.Notifications.Channels
{
    public class DiscordNotificationChannel : INotificationChannel
    {
        private readonly IDiscordService _discordService;

        public DiscordNotificationChannel(IDiscordService discordService)
        {
            _discordService = discordService;
        }

        public async Task NotifyAsync(Product product)
        {
            await _discordService.SendMessageAsync(
                $"🎉 Le produit **{product.ProductName}** est maintenant disponible !"
            );
        }

        public string GetChannelName() => "Discord";
    }
}
