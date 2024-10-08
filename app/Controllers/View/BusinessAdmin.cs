using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;

namespace scheapp.app.Controllers.View
{
    [Authorize(Roles = "scheappadmin")]
    public class BusinessAdminController : Controller
    {
        public BusinessAdminController(IProfessionalDataService professionalDataService)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
