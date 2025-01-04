using Microsoft.AspNetCore.Mvc;

namespace PROJECTGREEN.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
