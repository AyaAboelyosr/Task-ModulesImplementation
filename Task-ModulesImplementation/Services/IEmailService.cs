namespace Task_ModulesImplementation.Services
{
    public interface IEmailService
    {
        Task sendEmailAsync(string mailTo, string subject, string body=null, IList<IFormFile> attatchmemts = null);
        

        
    }
}
