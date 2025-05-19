using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using scheapp.app.Areas.Identity;
using scheapp.app.Areas.ScheApp.Pages.BusinessAdmin;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.DspModels;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace scheapp.app.Controllers.View
{
    [Authorize]
    public class ProfessionalRegisterController : Controller
    {
        private readonly ILogger<ProfessionalRegisterController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProfessionalDataService _professionalDataService;
        private readonly IBusinessDataService _businessDataService;
        public ProfessionalRegisterController(UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<ProfessionalRegisterController> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager
            , IProfessionalDataService professionalDataService
            , IBusinessDataService businessDataService
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _professionalDataService = professionalDataService;
            _businessDataService = businessDataService;
        }
        public async Task<IActionResult> Register(string registrationAttempMessage)
        {
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ProfessionalRegistrationVM professionalRegistrationModel = new ProfessionalRegistrationVM();
            professionalRegistrationModel.RegistrationAttempMessage = registrationAttempMessage;
            if (User.IsInRole("business_admin"))
            {
                List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, null);

                allProfessionalBusinessDetails = allProfessionalBusinessDetails.Where(p => p.AspNetUserName == User.Identity.Name).ToList();
                professionalRegistrationModel.BusinessList = allProfessionalBusinessDetails.Select(i => new SelectListItem
                    {
                        Text = i.BusinessName,
                        Value = i.BusinessId.GetValueOrDefault().ToString()
                    });
            }
            else
            {
                var businesses = await _businessDataService.GetBusinesses();
                professionalRegistrationModel.BusinessList = businesses.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(professionalRegistrationModel);
        }

        public async Task<IActionResult> SubmitRegistration(IFormCollection keyValuePairs)
        {
            try
            {
                string returnUrl = "/";
                var Input = new ProfessionalRegistrationVM();
                string firstname = keyValuePairs["Firstname"].ToString();
                string lastname = keyValuePairs["Lastname"].ToString();
                string email = keyValuePairs["Email"].ToString();
                string password = keyValuePairs["Password"].ToString();
                string businessId = keyValuePairs["BusinessId"].ToString();
                var checkUser = await _userManager.FindByEmailAsync(email);
                if (checkUser == null) /// user with that email do not exists
                {
                    var user = CreateUser();

                    await _userStore.SetUserNameAsync(user, email, CancellationToken.None);

                    user.Firstname = firstname;
                    user.Lastname = lastname;
                    user.Email = email;
                    user.NormalizedEmail = email.ToUpper();
                    user.EmailConfirmed = true;
                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded) 
                    {
                        _logger.LogInformation("User created a new account with password.");

                        await _userManager.AddToRoleAsync(user, "business_professional");
                        var userId = await _userManager.GetUserIdAsync(user);
                        var result2 = await _professionalDataService.SaveProfessionals(new Models.Data.TableModels.Professionals.Professional
                        {
                            FirstName = user.Firstname,
                            MiddleName = "",
                            LastName = user.Lastname,
                            AspNetUserId = userId,
                            BusinessId = Convert.ToInt32(businessId)
                        });
                        return Redirect("/BusinessAdmin");
                    }
                }
                else
                {
                    returnUrl = "/ProfessionalRegister/Register?registrationAttempMessage=Email Already Exists.";
                }

                // If we got this far, something failed, redisplay form
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
    public class ProfessionalRegistrationVM
    {
        public string RegistrationAttempMessage { get; set; }
        public string ReturnUrl { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string BusinessId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> BusinessList { get; set; }
    }
}
