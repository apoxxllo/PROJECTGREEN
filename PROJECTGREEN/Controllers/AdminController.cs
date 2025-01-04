using Microsoft.AspNetCore.Mvc;

namespace PROJECTGREEN.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
