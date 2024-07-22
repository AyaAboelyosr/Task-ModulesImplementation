﻿namespace Task_ModulesImplementation.ViewModels
{
    public class MailRequestViewModel
    {
        
        public string ToEmail { get; set; }
     
        public string Subject { get; set; }

        public string Body { get; set; }

        public IList<IFormFile> Attachments { get; set; }
    }
}