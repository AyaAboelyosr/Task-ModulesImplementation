using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using Task_ModulesImplementation.Models;

namespace Task_ModulesImplementation.ViewModels
{
    public class AddDepartmentViewModel
    {

        public string Name { get; set; }
        public IFormFile Logo { get; set; }
        public int? parentDepartmentId { get; set; }
         public List<Department>? parentDepartmentList { get; set; }
        
    }
}
