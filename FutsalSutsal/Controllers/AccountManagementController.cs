using Futsal.Entities;
using Futsal.Services;
using FutsalSutsal.Identity;
using FutsalSutsal.Models;

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace haei.client.Controllers
{
    //[Authorize(Roles = "Admin,Manager")]
    public class AccountManagementController : Controller
    {
        private readonly IUserAccountManager _userAccountManager;
        private readonly IIdentityManager _identityManager;
        private readonly ApplicationUserManager _applicationUserManager;

        public AccountManagementController(
            IIdentityManager identityManager
            , IUserAccountManager userAccountManager
             , ApplicationUserManager applicationUserManager)
        {
            _identityManager = identityManager;
            _userAccountManager = userAccountManager;
            _applicationUserManager = applicationUserManager;
        }


        #region Role Configuration

        public async Task<ActionResult> SearchRoles()
        {

            var roles = await _identityManager.GetAllRolesAsync();
            var model = new List<RoleViewModel>();

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    model.Add(new RoleViewModel { Id = role.Id, Name = role.Name, IsDeleted = role.IsDeleted });
                }
            }


            return View(model);
        }

        //GET Add roles
        public ActionResult AddRole() => View();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddRole(RoleViewModel roleViewModel)
        {
            int userId = User.Identity.GetUserId<int>();
            bool isRoleAdded = await _identityManager.CreateRoleAsync(roleViewModel.Name, userId);
            if (isRoleAdded)
                return RedirectToAction(nameof(SearchRoles));
            else
                return View(roleViewModel);
        }

        //edit roles
        public async Task<ActionResult> EditRole(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var role = await _identityManager.FindRoleAsync(id);
            if (role == null) return HttpNotFound();

            var model = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditRole(RoleViewModel model)
        {
            var aspnetrole = await _identityManager.FindRoleAsync(model.Id);
            if (aspnetrole == null) return HttpNotFound();

            aspnetrole.Name = model.Name;
            aspnetrole.LastUpdatedByUser = await _userAccountManager.GetLoggedInUserId();
            aspnetrole.LastUpdatedDate = DateTime.Now;
            aspnetrole.IsDeleted = false;

            bool isRoleAdded = await _identityManager.UpdateRoleAsync(aspnetrole);
            if (isRoleAdded)
                return RedirectToAction(nameof(SearchRoles));
            else
                return View(model);
        }

        public async Task<ActionResult> DeleteRole(int id)
        {
            if (id != 0)
            {
                var aspnetrole = await _identityManager.FindRoleAsync(id);
                aspnetrole.IsDeleted = true;
                aspnetrole.LastUpdatedDate = DateTime.Now;
                aspnetrole.LastUpdatedByUser = await _identityManager.GetLoggedInUserIdAsync();
                await _identityManager.UpdateRoleAsync(aspnetrole);
            }
            return RedirectToAction(nameof(SearchRoles));
        }



        #endregion


        #region User Configuration
        // GET: User
        public async Task<ActionResult> SearchUsers()
        {
            var aspnetuser = await _identityManager.GetAllUsersAsync();
            var searchUserVm = new List<SearchUserViewModel>();
            foreach (var item in aspnetuser)
            {
                searchUserVm.Add(new SearchUserViewModel
                {
                    Id = item.Id,
                    Username = item.UserName,
                    Firstname = item.FirstName,
                    Lastname = item.LastName,
                    MiddleInitial = item.MiddleInitial,
                    Email = item.Email,
                    IsUserActive = item.IsUserActive,
                    FullAddress = $"{item.Address1} {item.Address2} {item.City}, {item.Province} {item.District} {item.PostalCode}",
                    Fullname = $"{item.LastName}, {item.MiddleInitial} {item.FirstName}"
                });
            }
            return View(searchUserVm);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aspNetUser = await _identityManager.FindUserAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: User/Create
        public ActionResult Create() => View();

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddEditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var aspnetuser = new AspNetUser()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    Province = model.Province,
                    District = model.District,
                    PostalCode = model.PostalCode, 
                    IsUserActive = model.IsUserActive,
                    Email = model.Email,
                    EmailConfirmed=true, //set
                    PasswordHash = new PasswordHasher().HashPassword(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = false, //set
                    AlternatePhoneNumber = model.AlternatePhoneNumber.ToString(),
                    TwoFactorEnabled= true, //set
                    LockoutEnabled =false,
                    AccessFailedCount=0,
                    CreatedDate= DateTime.Now, 
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedByUser =await  _identityManager.GetLoggedInUserIdAsync()
                };
                bool isUserCreated = await _identityManager.CreateUserAsync(aspnetuser);
                return RedirectToAction(nameof(SearchUsers));
            }

            return View(model);
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aspNetUser = await _identityManager.FindUserAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                await _identityManager.UpdateUserAsync(aspNetUser);
                return RedirectToAction(nameof(SearchUsers));
            }
            return View(aspNetUser);
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aspNetUser = await _identityManager.FindUserAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            bool result = await _identityManager.DeleteUserAsync(id);

            return RedirectToAction(nameof(SearchUsers));
        }


        #endregion


        #region User-Role Configuration 
        [HttpGet]
        public async Task<ActionResult> SearchUserRoles()
        {
            var userRoles = await _userAccountManager.GetAllActiveUserRoles();
            var model = new List<SearchUserRoleViewModel>();
            if (userRoles != null)
            {
                try
                {
                    foreach (var item in userRoles)
                    {
                        model.Add(new SearchUserRoleViewModel { UserId = item.UserId, RoleId = item.RoleId.Value, UserRoleId = item.UserRoleId.Value, Username = item.UserName, Fullname = item.FullName, Role = item.Role, RoleAssignedByUser = item.RoleAssignedBy, RoleAssignedDate = item.RoleAssignedDate.Value });
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> EditUserRole(int userRoleId)
        {
            try
            {

                //get user role with userroleid provided 
                var userRole = await _userAccountManager.GetUserRoleByIdAsync(userRoleId);
                var user = await _userAccountManager.GetUserByIdAsync(userRole.UserId);

                var roles = await _identityManager.GetAllRoleSelectListItems();
                var model = new AddEditUserRoleViewModel { Id = userRoleId, UserId = userRole.UserId, User = user.UserName, RoleSelectListItems = roles };
                return View(model);
            }
            catch (Exception ex )
            {
                throw ex ;
            }

        }

        [HttpPost]
        public async Task<ActionResult> EditUserRole(int userRoleId, string role) => View();


        [HttpGet]
        public async Task<ActionResult> DeleteUserRole(int userRoleId) => View();

        [HttpPost]
        public async Task<ActionResult> DeleteUserRole(int userRoleId, string role) => View();




        [HttpGet]
        public async Task<ActionResult> ConfigureUserToRole()
        {
            var roles = await _identityManager.GetAllRoleSelectListItems();
            var users = _identityManager.GetAllActiveUserSelectListItems();

            var model = new UserRoleViewModel
            {
                RoleSelectListItems = roles,
                UserSelectListItems = users,
                RoleId = roles.Where(p => p.Selected == true).First().Value,
                UserId = Convert.ToInt32(users.Where(p => p.Selected == true).First().Value)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfigureUserToRole(int userId, string role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _identityManager.AssignRoleToUser(userId, role, await _identityManager.GetLoggedInUserIdAsync());
                    if (result == false)
                        throw new ArgumentNullException("Cannot assign role to the user");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction(nameof(SearchUserRoles));
        }






        #endregion User-Role Configuration 













        // GET: AccountManagement
        public ActionResult Index() => View();
    }
}