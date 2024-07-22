using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.ViewModels
{
    public class DetailsDepartmentViewModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public List<Department> parentDepartments { get; set; }
        public List<Department> subDepartments { get; set; }
    }
}
