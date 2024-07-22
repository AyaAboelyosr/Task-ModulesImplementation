namespace Task_ModulesImplementation.Services
{
    public interface IEmailJob
    {
        Task SendScheduledEmail(string toEmail, string subject);
    }
}
