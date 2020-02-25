using Futsal.Web.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Futsal.Web.Models
{
    public class UserRegisterViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "User name*")]
        [Required(ErrorMessage = "Enter username")]
        public string UserName { get; set; }

        [Display(Name = "Email address*")]
        [Required(ErrorMessage = "Enter email address")]
        [StringLength(maximumLength: 50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "please enter valid email address")]
        public string Email { get; set; }

        [Display(Name = "First name*")]
        [Required(ErrorMessage = "Enter first name")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 50, ErrorMessage = "First name must be less than 50 char length")]
        public string FirstName { get; set; }

        [Display(Name = "Last name*")]
        [Required(ErrorMessage = "Enter last name")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 50, ErrorMessage = "Last name must be less than 50 char length")]
        public string LastName { get; set; }

        [Display(Name = "Middle Initial")]
        [DataType(DataType.Text)]
        [StringLength(1, ErrorMessage = "Enter middle initial only (1 char)")]
        public string MiddleInitial { get; set; }

        [Display(Name = "Street Address*")]
        [Required(ErrorMessage = "Enter valid street address")]
        [StringLength(maximumLength: 50, ErrorMessage = "Street address must be less than 50 char length")]
        public string Address1 { get; set; }

        [Display(Name = "Address")]
        public string Address2 { get; set; }

        [Display(Name = "City*")]
        [Required(ErrorMessage = "Enter city name")]
        [StringLength(maximumLength: 50, ErrorMessage = "city name is too long")]
        public string City { get; set; }

        [Display(Name = "District*")]
        [Required(ErrorMessage = "Enter district name")]
        [StringLength(maximumLength: 50, ErrorMessage = "district name is too long")]
        public string District { get; set; }

        [Display(Name = "Province*")]
        [Required(ErrorMessage = "Enter province name")]
        [StringLength(maximumLength: 50)]
        public string Province { get; set; }

        [Display(Name = "Postal code*")]
        [Required(ErrorMessage = "Enter postal code")]
        [DataType(DataType.PostalCode, ErrorMessage = "postal code is a number, you dummy")]
        public int PostalCode { get; set; }

        [Display(Name = "Phone # *")]
        [Required(ErrorMessage = "Enter contact #")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Enter valid phone #")]
        [StringLength(maximumLength: 10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Alternate Phone #")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "phone number is a number, jack ass")]
        [StringLength(maximumLength: 10, ErrorMessage = "Enter valid phone#")]
        public string AlternatePhoneNumber { get; set; }

        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }


    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }


    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) => Name = name;
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}