
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Task_ModulesImplementation.Settings;

namespace Task_ModulesImplementation.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(Microsoft.Extensions.Options.IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task sendEmailAsync(string mailTo, string subject, string body = null, IList<IFormFile> attatchmemts = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject,
            };
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            if (attatchmemts != null)
            {
                foreach (var att in attatchmemts)
                {
                    if (att.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        att.CopyTo(memoryStream);
                        builder.Attachments.Add(att.FileName, memoryStream.ToArray(), ContentType.Parse(att.ContentType));
                    }
                }
            }
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    

}
}
