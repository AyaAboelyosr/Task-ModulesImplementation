using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_ModulesImplementation.Models;
using Task_ModulesImplementation.Repository;
using Task_ModulesImplementation.Services;

namespace Task_ModulesImplementation.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IEmailJob _emailJob;
        private readonly IRemiderRepository _remiderRepository;

        public ReminderController(IEmailJob emailJob , IRemiderRepository remiderRepository)
        {
            _emailJob = emailJob;
            _remiderRepository = remiderRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Reminder> reminders =_remiderRepository.GetAll();

            return View(reminders);
        }

        [HttpGet]
        public IActionResult CreateReminder()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateReminder(Reminder reminder)
        {
            if(ModelState.IsValid)
            {
                _remiderRepository.Insert(reminder);
                _remiderRepository.Save();

                var timeSpan = reminder.ReminderDateTime - DateTime.Now;
                Console.WriteLine($"Scheduling email to be sent in: {timeSpan.TotalMinutes} minutes");
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                if (!string.IsNullOrEmpty(userEmail))
                {
                    BackgroundJob.Schedule(() =>
                        _emailJob.SendScheduledEmail(userEmail, reminder.Title),
                        timeSpan);
                }


                return RedirectToAction("Index");
            }
            return View(reminder);
        }

    }
}
