using Microsoft.AspNetCore.Identity;

namespace scheapp.app.Areas.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = null!;
    }
}
