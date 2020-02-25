using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Futsal.Web.Models
{

    public class SearchUserRoleViewModel
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string RoleAssignedByUser { get; set; }
        public DateTime RoleAssignedDate { get; set; }




    }
    public class AddEditUserRoleViewModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string User { get; set; }
        public string Role { get; set; }

        [Display(Name = "Select User")]
        public IEnumerable<SelectListItem> UserSelectListItems { get; set; }


        [Display(Name = "Select Role")]
        public IEnumerable<SelectListItem> RoleSelectListItems { get; set; }


    }
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class SearchUserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MiddleInitial { get; set; }
        public string FullAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int PostalCode { get; set; }
        public bool IsUserActive { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
    }

    public class AddEditUserViewModel : UserRegisterViewModel
    {
        //[Display(Name = "Address")] public string Address1 { get; set; }
        //[Display(Name = "Address")] public string Address2 { get; set; }

        //[Display(Name = "Phone # *")]
        //[DataType(DataType.PhoneNumber)]
        //[Required(ErrorMessage = "Phone # required.")]
        //public string PhoneNumber { get; set; }

        //[Display(Name = "Alternate #")]
        //[DataType(DataType.PhoneNumber)]
        //public int AlternatePhoneNumber { get; set; }

        [Display(Name = "Email Confirmed?")] public bool IsEmailConfirmed { get; set; } = true;
        [Required(ErrorMessage = "User activation required.")] [Display(Name = "User Active ? *")] public bool IsUserActive { get; set; } = true;
        [Display(Name = "Phone Confirmed?")] public bool IsPhoneNumberConfirmed { get; set; } = true;


        //[Display(Name = "Password *")]
        //[Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        //[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //public string Password { get; set; }
    }



    //user roles 

    public class UserRoleViewModel
    {
        [Required]
        [Display(Name = "Select User")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Select Role")]
        public string RoleId { get; set; }

        public string Role { get; set; }


        [Display(Name = "Select User")]
        //  public string User { get; set; }
        public IEnumerable<SelectListItem> UserSelectListItems { get; set; }

        [Display(Name = "Select Role")]
        // public string Role { get; set; }
        public IEnumerable<SelectListItem> RoleSelectListItems { get; set; }
    }
}