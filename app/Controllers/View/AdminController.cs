using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.Controllers.Data;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.TableModels.Businesses;

namespace scheapp.app.Controllers.View
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "scheapp_admin")]
    public class AdminController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalsDataService;
        private readonly IBusinessDataService _businessDataService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(ILogger<AdminController> logger
            , IProfessionalDataService professionalsDataService
            , IBusinessDataService businessDataService
            , RoleManager<IdentityRole> roleManager
            , UserManager<IdentityUser> userManager
            )
        {
            _logger = logger;
            _professionalsDataService = professionalsDataService;
            _businessDataService = businessDataService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Businesses()
        {
            var businesses = _businessDataService.GetBusinesses().Result;
            return View(businesses);
        }
        public IActionResult BusinessDetails(int businessId)
        {
            
            return View();
        }
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public IActionResult CreateNewRole()
        {
            return View();
        }
        public IActionResult CreateNewBusiness()
        {
            return View();
        }
        public IActionResult UserRoles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            //avoid duplicacy
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBusiness(Business newBusiness)
        {
            await _businessDataService.SaveBusinesses(newBusiness);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetMessage(string name)
        {
            return Ok($"CommunicationController: Namaste {name}!");
        }
    }
}
