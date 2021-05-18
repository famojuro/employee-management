using System;
using System.IO;
using EmployeeManagement.manager;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IEmployeeManager mockEmployeeManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _employeeManager = mockEmployeeManager;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            var model = _employeeManager.GetAllEmployees();

            return View(model);
        }
        
        public IActionResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeManager.GetEmployeeDetails(id ?? 1),
                PageTitle = "Employee Details"
            };
           
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                   string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                   uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                   string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                   model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeManager.CreateEmployee(newEmployee);
            
                return RedirectToAction("details", new {id = newEmployee.Id});    
            }
            
            return View();
        }
    }
}