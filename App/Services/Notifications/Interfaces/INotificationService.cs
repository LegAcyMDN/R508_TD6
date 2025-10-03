using App.Models;

namespace App.Services.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task NotifyProductAvailableAsync(Product product);
        void AddChannel(INotificationChannel channel);
        void RemoveChannel(string channelName);
    }
}
