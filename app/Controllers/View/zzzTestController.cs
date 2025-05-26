using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace scheapp.app.Controllers.View
{
    [Authorize]
    public class zzzTestController : Controller
    {
        public IActionResult BootstrapTable()
        {
            return View();
        }
        public IActionResult CameraAccess()
        {
            return View();
        }
    }
}
