using Microsoft.AspNetCore.Mvc;
using PROJECTGREEN.Models;
using System.Diagnostics;

namespace PROJECTGREEN.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult WasteManagement()
        {
            return View();
        }

        public IActionResult Organization()
        {
            return View();
        }

        public IActionResult CommunityInvolvementPrograms()
        {
            return View();
        }

        public IActionResult Incidents()
        {
            return View();
        }


    }
}
