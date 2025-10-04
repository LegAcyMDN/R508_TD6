namespace App.Repositories.Interfaces
{
    public interface IDiscordService
    {
        Task SendMessageAsync(string message);
    }
}
