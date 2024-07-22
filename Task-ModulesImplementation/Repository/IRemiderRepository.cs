using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.Repository
{
    public interface IRemiderRepository
    {
        List<Reminder> GetAll();
        Reminder GetById(int id);

        void Insert(Reminder obj);

        void Update(Reminder obj);

        void Delete(int id);
        string UploadFile(IFormFile formFile);

        void Save();
    }
}
