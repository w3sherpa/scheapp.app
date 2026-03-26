using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.API;

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
            ProfessionalServiceRS vm = new ProfessionalServiceRS();
            vm.BusinessId = businessId;
            return View(vm);
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
        public async Task<IActionResult> CreateNewBusiness([FromBody] CreateBusinessRQ newBusiness)
        {
            await _businessDataService.SaveBusinesses(new Business
            {
                Name = newBusiness.Name
                ,
                Website = newBusiness.Website
                ,
                StreetOne = newBusiness.StreetOne
                ,
                StreetTwo = newBusiness.StreetTwo
                ,
                City = newBusiness.City
                ,
                State = newBusiness.State
                ,
                ZipCode = newBusiness.ZipCode
                ,
                Country = newBusiness.Country
                ,
                IsActive = newBusiness.IsActive.Trim().ToUpper() == "TRUE" ? true : false
            });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetMessage(string name)
        {
            return Ok(new { Message = "CommunicationController: Namaste {name}!" });
        }
        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageRQ req)
        {
            return Ok(new { Message = $"CommunicationController: Get the message {req.Message}!" });
        }
    }
}
