using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SessionTestProject.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult CreateError(string errorMessage)
        {
            ViewData["ErrorMessage"] = errorMessage;
            return View("CreateError");
        }

        [HttpPost]
        public IActionResult GoCreatePage()
        {
            return RedirectToAction("Create", "Account");
        }

    }
}
