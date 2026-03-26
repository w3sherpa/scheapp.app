using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using scheapp.app.Areas.Identity;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.API;


internal static class ProgramDataSeeder
{
    public static async Task SeedSecuritySherpa(IServiceProvider serviceProvider  )
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            ////Get Config data
            ///

            string scheAppAdminRoleName = configuration["ScheAppAdminRoleName"];
            string scheAppBusinessAdminRoleName = configuration["ScheAppBusinessAdminRoleName"];
            string scheAppBusinessProfessionalRoleName = configuration["ScheAppBusinessProfessionalRoleName"];

            string scheaAppScheAppAdminEmail = configuration["ScheAppAdminEmail"];

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var userStore = scope.ServiceProvider.GetRequiredService<IUserStore<IdentityUser>>();
            var professionalDataService = scope.ServiceProvider.GetRequiredService<IProfessionalDataService>();

            if (!roleManager.RoleExistsAsync(scheAppAdminRoleName).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(scheAppAdminRoleName)).GetAwaiter().GetResult();
            }

            if (!roleManager.RoleExistsAsync(scheAppBusinessAdminRoleName).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(scheAppBusinessAdminRoleName)).GetAwaiter().GetResult();
            }

            if (!roleManager.RoleExistsAsync(scheAppBusinessProfessionalRoleName).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(scheAppBusinessProfessionalRoleName)).GetAwaiter().GetResult();
            }

            var scheappAdminUser = new ApplicationUser();

            await userStore.SetUserNameAsync(scheappAdminUser, scheaAppScheAppAdminEmail, CancellationToken.None);

            scheappAdminUser.Firstname = "ScheApp";
            scheappAdminUser.Lastname = "Sherpa";
            scheappAdminUser.Email = scheaAppScheAppAdminEmail;
            scheappAdminUser.NormalizedEmail = scheaAppScheAppAdminEmail.ToUpper();
            scheappAdminUser.EmailConfirmed = true;
            var result = await userManager.CreateAsync(scheappAdminUser, "$cheapPp5h3rpa");
            if (result.Succeeded)
            {
                Console.WriteLine("User created a new account with password.");

                await userManager.AddToRoleAsync(scheappAdminUser, scheAppAdminRoleName);
                var userId = await userManager.GetUserIdAsync(scheappAdminUser);
                var result2 = await professionalDataService.SaveProfessionals(new Professional
                {
                    FirstName = scheappAdminUser.Firstname,
                    MiddleName = "",
                    LastName = scheappAdminUser.Lastname,
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