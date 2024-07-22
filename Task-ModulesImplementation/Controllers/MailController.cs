using Microsoft.AspNetCore.Mvc;
using Task_ModulesImplementation.Services;
using Task_ModulesImplementation.ViewModels;

namespace Task_ModulesImplementation.Controllers
{
    public class MailController : Controller
    {
        private readonly IEmailService _mailService;
        public MailController(IEmailService mailService)
        {
            _mailService = mailService;
        }
        public async Task< IActionResult> SendMail([FromForm]MailRequestViewModel mailRequestViewModel)
        {
            await _mailService.sendEmailAsync(mailRequestViewModel.ToEmail, mailRequestViewModel.Subject, mailRequestViewModel.Body, mailRequestViewModel.Attachments);
            return Ok();
        }
    }
}
