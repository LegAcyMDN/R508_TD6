namespace App.Repositories.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string subject, string body);
    }
}
