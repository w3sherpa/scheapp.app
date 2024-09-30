using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace scheapp.app.Controllers.View
{
    [Authorize(Roles = "scheappadmin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
