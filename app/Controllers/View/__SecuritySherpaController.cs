using Microsoft.AspNetCore.Mvc;

namespace scheapp.app.Controllers.View
{
    public class SecuritySherpaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
