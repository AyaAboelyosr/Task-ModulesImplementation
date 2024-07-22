using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        List<Department> GetSubDepartment(int id);
        List<Department> GetParentDepartment(int id);


        Department GetById(int id);

        void Insert(Department obj);

        void Update(Department obj);

        void Delete(int id);
        string UploadFile(IFormFile formFile);

        void Save();


    }
}
