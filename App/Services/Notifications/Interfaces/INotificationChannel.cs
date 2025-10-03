using App.Models;

namespace App.Services.Notifications.Interfaces
{
    public interface INotificationChannel
    {
        Task NotifyAsync(Product product);
        string GetChannelName();
    }
}
