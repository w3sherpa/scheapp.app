using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using scheapp.app.Areas.Identity;
using scheapp.app.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace scheapp.app.Controllers.View
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(ILogger<AuthController> logger
            , SignInManager<IdentityUser> signInManager
            )
        {
            _logger = logger;
            _signInManager = signInManager; 
        }
        /// <summary>
        /// Anonymous user will land here first, gets redirected to external login, then comes back to ExternalLoginCallback where user will be 
        /// redirected here again, so we can redirect them to proper page based on the role they have. 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl,string loginAttempMessage)
        {
            var loggedInUser = User.Identity;
            if (loggedInUser != null && !loggedInUser.IsAuthenticated)
            {
                ScheAppAuthModel model = new ScheAppAuthModel
                {
                    ReturnUrl = "/auth/Login/",
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                };

                return View(model);
            }
            else
            {
                var redirectUrl = User.IsInRole("scheapp_admin") ? "/businessadmin/index" : "/Professionals/Schedules";
                return LocalRedirect(redirectUrl);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ScheappLogin(string email, string password,bool rememberMe)
        {
            string returnUrl = string.Empty;
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                // redirect user based on role
                var signInUser = await _signInManager.UserManager.FindByNameAsync(email);
                var userRoles = await _signInManager.UserManager.GetRolesAsync(signInUser);
                //var allRoles = _roleManager.Roles.Select(R=>R.Name).ToList();
                if (userRoles.Where(ur => ur.StartsWith("scheapp")).FirstOrDefault() != null) returnUrl = "/Admin/Index";
                else if (userRoles.Where(ur => ur.Equals("business_admin")).FirstOrDefault() != null) returnUrl = "/BusinessAdmin/Index";
                else if (userRoles.Where(ur => ur.Equals("business_professional")).FirstOrDefault() != null) returnUrl = "/Professionals/Schedules";
            }
            else
            {
                returnUrl = "/Auth/Login?returnUrl=&loginAttempMessage=failed";
            }
            return LocalRedirect(returnUrl);
        }

            

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            //This call will generate a URL that directs to the ExternalLoginCallback action method in the Account controller
            //with a route parameter of ReturnUrl set to the value of returnUrl.
            var redirectUrl = Url.Action(action: "ExternalLoginCallback", controller: "Auth", values: new { ReturnUrl = returnUrl });

            // Configure the redirect URL, provider and other properties
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            //This will redirect the user to the external provider's login page
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl, string? remoteError)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ScheAppAuthModel loginViewModel = new ScheAppAuthModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", loginViewModel);
            }
            // Get the login information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", loginViewModel);
            }
            // If the user already has a login (i.e., if there is a record in AspNetUserLogins table)
            // then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _signInManager.UserManager.FindByNameAsync(email);
                    if (user == null)
                    {
                        var applicationUser = CreateUser();
                        string userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                        applicationUser.Email = userEmail;
                        applicationUser.UserName = userEmail;
                        applicationUser.Firstname = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                        applicationUser.Lastname = info.Principal.FindFirstValue(ClaimTypes.Surname);
                        var result = await _signInManager.UserManager.CreateAsync(applicationUser);
                        await _signInManager.UserManager.AddLoginAsync(user, info);
                        //Then Signin the User
                        await _signInManager.SignInAsync(applicationUser, isPersistent: false);

                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                   
                    return LocalRedirect(returnUrl);
                    // Add a login (i.e., insert a row for the user in AspNetUserLogins table)

                }
                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on info@dotnettutorials.net";
                return View("Error");
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
    public class ScheAppAuthModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string LoginMessage { get; set; } = "";
        public string ReturnUrl { get; set; } = "";
        public IList<AuthenticationScheme> ExternalLogins { get; set; } 
    }
}
