using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using scheapp.app.Areas.Identity;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.TableModels.Professionals;

internal static class ProgramDataSeeder
{
    public static async Task SeedSecuritySherpa(
         IServiceProvider serviceProvider  )
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            ////Get Config data
            ///

            string securitySherpaRole = configuration["SecuritySherpaRole"];
            string scheaAppSecuritySherpaEmail = configuration["SecuritySherpaEmail"];

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var userStore = scope.ServiceProvider.GetRequiredService<IUserStore<IdentityUser>>();
            var professionalDataService = scope.ServiceProvider.GetRequiredService<IProfessionalDataService>();

            if (!roleManager.RoleExistsAsync(securitySherpaRole).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(securitySherpaRole)).GetAwaiter().GetResult();
            }

            var securitySherpaUser = new ApplicationUser();

            await userStore.SetUserNameAsync(securitySherpaUser, scheaAppSecuritySherpaEmail, CancellationToken.None);

            securitySherpaUser.Firstname = "ScheApp";
            securitySherpaUser.Lastname = "Security-Sherpa";
            securitySherpaUser.Email = scheaAppSecuritySherpaEmail;
            securitySherpaUser.NormalizedEmail = scheaAppSecuritySherpaEmail.ToUpper();
            securitySherpaUser.EmailConfirmed = true;
            var result = await userManager.CreateAsync(securitySherpaUser, "$cheapPp5h3rpa");
            if (result.Succeeded)
            {
                Console.WriteLine("User created a new account with password.");

                await userManager.AddToRoleAsync(securitySherpaUser, securitySherpaRole);
                var userId = await userManager.GetUserIdAsync(securitySherpaUser);
                var result2 = await professionalDataService.SaveProfessionals(new Professional
                {
                    FirstName = securitySherpaUser.Firstname,
                    MiddleName = "",
                    LastName = securitySherpaUser.Lastname,
                    AspNetUserId = userId,
                    BusinessId = 1
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}