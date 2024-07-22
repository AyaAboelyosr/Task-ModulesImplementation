namespace Task_ModulesImplementation.Models
{
    public class Department
    {
        public int  Id { get; set; }
        public string Name { get; set; } 
        public string Logo { get; set; }

        public int? parentDepartmentId {  get; set; }
        public Department parentDepartment {  get; set; }

        public List<Department> subDepartments { get; set; }

     
    }
}
