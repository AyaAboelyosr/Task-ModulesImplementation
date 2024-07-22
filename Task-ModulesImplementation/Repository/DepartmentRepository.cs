using Microsoft.EntityFrameworkCore;
using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.Repository
{
    public class DepartmentRepository: IDepartmentRepository
    {
        ApplicationContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public DepartmentRepository( ApplicationContext applicationContext, IWebHostEnvironment _webHostEnvironment ) { 
          context = applicationContext;
        webHostEnvironment = _webHostEnvironment;

        
        }
        public List<Department> GetAll()
        {
            return context.Department.ToList();
        }

        public List<Department> GetSubDepartment(int parentId)
        {
            return context.Department
                         .Where(d => d.parentDepartmentId == parentId)
                         .ToList();
        }

        public List<Department> GetParentDepartment(int id)
        {
            List<Department> parentDepartments = new List<Department>();
            Department currentDepartment = context.Department
                                                   .Include(d => d.parentDepartment)
                                                   .FirstOrDefault(d => d.Id == id);

            while (currentDepartment != null && currentDepartment.parentDepartment != null)
            {
                // Add the current department's parent to the list
                parentDepartments.Add(currentDepartment.parentDepartment);

                // Move to the next parent in the hierarchy and ensure it's fully loaded
                currentDepartment = context.Department
                                            .Include(d => d.parentDepartment)
                                            .FirstOrDefault(d => d.Id == currentDepartment.parentDepartment.Id);
            }

            return parentDepartments;
        }





        public Department GetById(int id)
        {
            return context.Department.Include(d => d.subDepartments)
                                    .Include(d => d.parentDepartment)
                                    .FirstOrDefault(d => d.Id == id);
        }
        public void Insert(Department obj)
        {
            context.Add(obj);
        }
        public void Update(Department obj)
        {
            context.Update(obj);
        }
        public void Delete(int id)
        {
            var department = context.Department
                               .Include(d => d.subDepartments)
                               .FirstOrDefault(d => d.Id == id);

            if (department != null)
            {
                // Set parentDepartmentId to null for all child departments
                foreach (var child in department.subDepartments)
                {
                    child.parentDepartmentId = null;
                }

                context.UpdateRange(department.subDepartments);
                context.Remove(department);
            }
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
            string filePath=Path.Combine(uploadPath, imageName);
            using(var fileStream= new FileStream(filePath, FileMode.Create))
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
