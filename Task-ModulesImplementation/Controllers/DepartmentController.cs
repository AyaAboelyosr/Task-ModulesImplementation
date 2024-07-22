using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using Task_ModulesImplementation.Models;
using Task_ModulesImplementation.Repository;
using Task_ModulesImplementation.ViewModels;

namespace Task_ModulesImplementation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _DepartmentRepository = departmentRepository;
            _webHostEnvironment = webHostEnvironment;


        }
        public IActionResult Index()
        {
            IEnumerable<Department> departments = _DepartmentRepository.GetAll();

            return View(departments);
        }





        //public IActionResult GetSubDeprtment(int Id) { 
        //    IEnumerable<Department> subDepartments= _DepartmentRepository.GetSubDepartment(Id);
        //    return View(subDepartments);    
        //}
        //public IActionResult GetParentDepartments(int Id) { 
        //    IEnumerable<Department> parentDepartments= _DepartmentRepository.GetParentDepartment(Id);
        //    return View(parentDepartments);

        //}
        [HttpGet]
        public IActionResult Details(int id)
        {

            Department department = _DepartmentRepository.GetById(id);

            var viewModel = new DetailsDepartmentViewModel
            {
                Name = department.Name,
                Logo = department.Logo,
                subDepartments = _DepartmentRepository.GetSubDepartment(id),
                parentDepartments = _DepartmentRepository.GetParentDepartment(id)
            };


            return View(viewModel);
        }
        public IActionResult Edit(int id)
        {
            Department department = _DepartmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            var viewModel = new EditDepartmentViewModel
            {
                Id = department.Id,
                Name = department.Name,
                parentDepartmentId = department.parentDepartmentId,
                parentDepartmentList = _DepartmentRepository.GetAll()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SaveEdit(EditDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = _DepartmentRepository.GetById(model.Id);
                if (department == null)
                {
                    return NotFound();
                }




                department.Logo = _DepartmentRepository.UploadFile(model.Logo); // Save the file and get the path


                department.Name = model.Name;
                department.parentDepartmentId = model.parentDepartmentId;

                _DepartmentRepository.Update(department);
                _DepartmentRepository.Save();
                return RedirectToAction("Index");
            }

            model.parentDepartmentList = _DepartmentRepository.GetAll();
            return View("Edit", model);
        }




        //[HttpGet]
        //public IActionResult AddNewDepartment()
        //{
        //    AddDepartmentViewModel viewModel = new AddDepartmentViewModel();
        //    //viewModel.Name= department.Name;
        //    //viewModel.Logo= _DepartmentRepository.UploadFile(department.Logo);
        //    viewModel.parentDepartmentList = _DepartmentRepository.GetAll();
        //    return View("AddNewDepartment");

        //}
        [HttpGet]
        public IActionResult AddNewDepartment()
        {
            var viewModel = new AddDepartmentViewModel
            {
                parentDepartmentList = _DepartmentRepository.GetAll()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SaveNewDepartment(AddDepartmentViewModel addDepartmentViewModel)
        {
            Department department1 = new Department();

            department1.Name = addDepartmentViewModel.Name;
            department1.Logo = _DepartmentRepository.UploadFile(addDepartmentViewModel.Logo);
            department1.parentDepartmentId = addDepartmentViewModel.parentDepartmentId;
            addDepartmentViewModel.parentDepartmentList = _DepartmentRepository.GetAll();
            if (ModelState.IsValid)
            {
                _DepartmentRepository.Insert(department1);
                _DepartmentRepository.Save();

                return RedirectToAction("Index");
            }
            return View(addDepartmentViewModel);


        }

        public IActionResult Delete(int id)
        {
            Department department = _DepartmentRepository.GetById(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            Department department = _DepartmentRepository.GetById(id);
            if (department == null)
            {
                return NotFound();
            }



            _DepartmentRepository.Delete(id);
            _DepartmentRepository.Save();


            return RedirectToAction("Index");
        }
    }
}
