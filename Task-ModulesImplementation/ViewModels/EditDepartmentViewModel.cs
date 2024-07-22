using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.ViewModels
{
    public class EditDepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Logo { get; set; }
        
        public int? parentDepartmentId { get; set; }
        public List<Department>? parentDepartmentList { get; set; }
    }
}
