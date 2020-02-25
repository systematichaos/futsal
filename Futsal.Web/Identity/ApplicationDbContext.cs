using Futsal.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Futsal.Web.Identity
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