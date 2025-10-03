using App.Models;
using App.Services.Notifications.Interfaces;

namespace App.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly List<INotificationChannel> _channels = new();

        public void AddChannel(INotificationChannel channel)
        {
            if (!_channels.Any(c => c.GetChannelName() == channel.GetChannelName()))
            {
                _channels.Add(channel);
            }
        }

        public void RemoveChannel(string channelName)
        {
            _channels.RemoveAll(c => c.GetChannelName() == channelName);
        }

        public async Task NotifyProductAvailableAsync(Product product)
        {
            var tasks = _channels.Select(channel => channel.NotifyAsync(product));
            await Task.WhenAll(tasks);
        }
    }
}
