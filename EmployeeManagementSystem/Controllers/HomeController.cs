using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_employeeRepository.GetAllEmployee());
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFoundError", id);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTittle = "Employee Details"
            };
            return View(homeDetailsViewModel);            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                ID = employee.ID,
                Name = employee.Name,
                Deparment = employee.Deparment,
                ExistenceFilePath = employee.PhotoPath,
                Email = employee.Email
            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.ID);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Deparment = model.Deparment;
                if (model.Photo != null)
                {
                    if(model.ExistenceFilePath != null)
                    {
                        string photoPath = Path.Combine(hostingEnvironment.WebRootPath, "Images", model.ExistenceFilePath);
                        System.IO.File.Delete(photoPath);
                    }

                    string uniqueFileName = FileUploadProcess(model);
                    employee.PhotoPath = uniqueFileName;
                }
                _employeeRepository.Update(employee);
                return RedirectToAction("Details", new { id = model.ID });
            }
            return View();
        }

        private string FileUploadProcess(HomeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string uploaddFile = Path.Combine(uploadFolderPath, uniqueFileName);
                using (FileStream fileStream = new FileStream(uploaddFile, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Delete(int id)
        {
            Employee emp = _employeeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(HomeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {

                string uploadFolderPath = FileUploadProcess(model);
                Employee emp = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Deparment = model.Deparment,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.AddEmployee(emp);
                return RedirectToAction("Details", new { id = emp.ID });
            }
            return View();
        }
    }
}