using Futsal.Data;
using Futsal.Entities;
using Futsal.Services.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Futsal.Services
{
    public interface IIdentityManager
    {
        Task<IEnumerable<AspNetRole>> GetAllRolesAsync();
        Task<bool> CreateRoleAsync(string name, int createdByUser);
        Task<AspNetRole> FindRoleAsync(int id);
        //Task<bool> DeleteRoleAsync(string id);
        Task<bool> UpdateRoleAsync(AspNetRole aspnetrole);

        Task<IEnumerable<AspNetUser>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(AspNetUser aspnetuser);
        Task<bool> UpdateUserAsync(AspNetUser aspNetUser);
        Task<AspNetUser> FindUserAsync(int id);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<AspNetUser>> GetAllActiveUsersAsync();
        IEnumerable<SelectListItem> GetAllActiveUserSelectListItems();
        Task<IEnumerable<SelectListItem>> GetAllRoleSelectListItems();
        bool AssignRoleToUser(int userId, string role, int loggedInUserId);

        Task<int> GetLoggedInUserIdAsync();
        Task<IEnumerable<UserRoles_Search_Result>> GetAllActiveUserRoles();
        string GetLoggedInUserName();
    }

    public class IdentityManager : IIdentityManager
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public IdentityManager(IIdentityRepository identityRepository, IUserAccountRepository userAccountRepository)
        {
            _identityRepository = identityRepository;
            _userAccountRepository = userAccountRepository;
        }

        //roles
        public async Task<IEnumerable<AspNetRole>> GetAllRolesAsync() => await _identityRepository.GetAllRolesAsync();
        public async Task<bool> CreateRoleAsync(string name, int createdByUser) => await _identityRepository.CreateRoleAsync(name, await GetLoggedInUserIdAsync());
        public async Task<AspNetRole> FindRoleAsync(int id) => await _identityRepository.FindRolesAsync(id);
        //public async Task<bool> DeleteRoleAsync(string id) => await _identityRepository.DeleteRoleAsync(id);
        public async Task<bool> UpdateRoleAsync(AspNetRole aspnetrole) => await _identityRepository.UpdateRoleAsync(aspnetrole);




        //aspnetusers
        public async Task<IEnumerable<AspNetUser>> GetAllUsersAsync()
            => await _userAccountRepository.GetAspNetUsersAsync();

        public async Task<IEnumerable<AspNetUser>> GetAllActiveUsersAsync()
            => await _userAccountRepository.GetAspNetUsersAsync();

        public async Task<bool> CreateUserAsync(AspNetUser aspnetuser)
            => await _userAccountRepository.PostAspNetUserAsync(aspnetuser);

        public async Task<bool> UpdateUserAsync(AspNetUser aspNetUser)
        {
            var user = await _userAccountRepository.UpdateAspNetUserAsync(aspNetUser);
            return user == null ? false : true;
        }

        public async Task<AspNetUser> FindUserAsync(int id)
            => await _userAccountRepository.FindAspNetUserAsync(id);

        public async Task<bool> DeleteUserAsync(int id)
            => await _userAccountRepository.DeleteAspNetUserAsync(id);


        #region DDL
        public IEnumerable<SelectListItem> GetAllActiveUserSelectListItems()
        {
            try
            {
                //  var users = _userAccountRepository.GetActiveAspNetUsers().Select(p => p.UserName).Distinct().ToList();
                var users = _userAccountRepository.GetActiveAspNetUsers().Select(p => new { p.FirstName, p.MiddleInitial, p.LastName, p.Id }).Distinct().ToList();

                var userSelectListItems = new List<SelectListItem>();
                if (users == null) return userSelectListItems; // if nothing, return empty list

                int i = 0;
                // to have first item selected, couldnt think of anything at this moment, if you can- refactor this
                foreach (var item in users)
                {
                    userSelectListItems.Add(new SelectListItem
                    {
                        Text = item.FirstName + " " + (string.IsNullOrEmpty(item.MiddleInitial) ? "" : item.MiddleInitial + " ") + item.LastName,
                        Value = item.Id.ToString(),
                        Selected = i == 0 ? true : false // have first item selected
                    });
                    i++;
                }
                return userSelectListItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllRoleSelectListItems()
        {
            try
            {
                var roles = await _identityRepository.GetAllRolesAsync();
                roles.Select(p => p.Name).Distinct().ToList();

                var roleSelectListItems = new List<SelectListItem>();
                if (roles == null || roles.Count() < 1) return roleSelectListItems; // if nothing, return empty list

                int i = 0;
                // to have first item selected, couldnt think of anything at this moment, if you can- refactor this
                foreach (var item in roles)
                {
                    roleSelectListItems.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = i == 0 ? true : false // have first item selected
                    });
                    i++;
                }
                return roleSelectListItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion DDL


        public bool AssignRoleToUser(int userId, string role, int loggedInUserId)
            => _userAccountRepository.AssignRoleToUser(userId, role, loggedInUserId);


        public async Task<int> GetLoggedInUserIdAsync()
        {
            int userId = 0;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated) // must be authenticated
            {

                if (System.Web.HttpContext.Current.User.IsImpersonating())
                {
                    return await _userAccountRepository.GetAspNetUserByIdAsync(System.Web.HttpContext.Current.User.GetOriginalID());
                }
                else
                {
                    return System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
                }

            }
            return userId;
        }

        public async Task<IEnumerable<UserRoles_Search_Result>> GetAllActiveUserRoles()
           => await _userAccountRepository.GetActiveUserRoles();

        public string GetLoggedInUserName()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated) // must be authenticated
            {

                if (System.Web.HttpContext.Current.User.IsImpersonating())
                {
                    return System.Web.HttpContext.Current.User.Identity.GetUserName();
                }
                else
                {
                    return System.Web.HttpContext.Current.User.Identity.GetUserName();
                }

            }
            return string.Empty;

        }
    }

}