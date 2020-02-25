using FutsalSutsal.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FutsalSutsal.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("Futsal")
        {
        }

        public static ApplicationDbContext Create() => new ApplicationDbContext();
    }

}