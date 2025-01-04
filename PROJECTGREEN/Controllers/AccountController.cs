using Microsoft.AspNetCore.Mvc;

namespace PROJECTGREEN.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
