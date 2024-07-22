using Microsoft.EntityFrameworkCore;
using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.Repository
{
    public class ReminderRepository : IRemiderRepository
    {

        ApplicationContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ReminderRepository(ApplicationContext applicationContext, IWebHostEnvironment _webHostEnvironment)
        {
            context = applicationContext;
            webHostEnvironment = _webHostEnvironment;


        }
        public List<Reminder> GetAll()
        {
            return context.Reminder.ToList();
        }

       
        

        public Reminder GetById(int id)
        {
            return context.Reminder.FirstOrDefault(e => e.Id == id);
        }
        public void Insert(Reminder obj)
        {
            context.Add(obj);
        }
        public void Update(Reminder obj)
        {
            context.Update(obj);
        }
        public void Delete(int id)
        {
             Reminder reminder = GetById(id);
            context.Remove(reminder);
        }

        public string UploadFile(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return null;
            }


            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string imageName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadPath, imageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return imageName;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
