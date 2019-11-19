using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{StatusCode}")]
        public IActionResult Index(int StatusCode)
        {
            switch (StatusCode)
            {
                case 404:
                    ViewBag.Message = "Sorry! The resource you requested is not found";
                    break;
            }
            return View("NotFound");
        }
    }
}