using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using scheapp.app.Areas.Identity;

namespace scheapp.app.Data;

public class ScheAppIdentityContext : IdentityDbContext<IdentityUser>
{
    public ScheAppIdentityContext(DbContextOptions<ScheAppIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
}
