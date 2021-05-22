using System;
using System.IO;
using EmployeeManagement.manager;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger _logger;

        public HomeController(IEmployeeManager employeeManager,
            IWebHostEnvironment webHostEnvironment,
            ILogger<HomeController> logger)
        {
            _employeeManager = employeeManager;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            var model = _employeeManager.GetAllEmployees();

            return View(model);
        }
        
        public IActionResult Details(int? id)
        {
            _logger.LogInformation("Employee details");
            Employee employee = _employeeManager.GetEmployeeDetails(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
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
                string uniqueFileName = ProcessUploadedFile(model);

                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeManager.CreateEmployee(newEmployee);
                _logger.LogInformation("new employee created");
                
                return RedirectToAction("details", new {id = newEmployee.Id});    
            }
            
            return View();
        }
        
        [HttpGet] 
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeManager.GetEmployeeDetails(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            
            return View(employeeEditViewModel);
        }
        
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeManager.GetEmployeeDetails(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                _employeeManager.UpdateEmployee(employee);
            
                return RedirectToAction("index");    
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _employeeManager.DeleteEmployee(id);
            return RedirectToAction("index");
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Photo.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}