using Futsal.Data;
using Futsal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Futsal.Services
{
    public interface IUserAccountManager
    {
        Task<IEnumerable<AspNetUser>> GetUsersAsync();
        Task<bool> CreateUserAsync(AspNetUser aspnetuser);
        Task<bool> IsAValidLoginUserAsync(string username, string email);

        /// <summary>
        /// Gets the asp net user user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<AspNetUser> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets the asp net user user asynchronous.
        /// </summary>
        /// <param name="id">The user id .</param>
        /// <returns></returns>
        Task<AspNetUser> GetUserByIdAsync(int id);

        /// <summary>
        /// Determines whether [the specified email] IS ALREADY REGISTERED UNDER ASPNET USER.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<bool> IsAspNetUser(string email);

        /// <summary>
        /// Updates the ASP net user.
        /// </summary>
        /// <param name="aspNetUser">The ASP net user.</param>
        /// <returns></returns>
        Task<bool> UpdateAspNetUserAsync(AspNetUser aspNetUser);

        /// <summary>
        /// Gets the asp net user user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<AspNetUser> GetUserAsync(string email);
        Task<AspNetUser> FindUserAsync(int id);

        Task InsertIntoUserLoginAttemptsAsync(UserLoginAttempt userLoginAttempt);
        Task<int> GetLoggedInUserId();


        Task<IEnumerable<UserRoles_Search_Result>> GetAllActiveUserRoles();
        /// <summary>
        /// Gets the user role by id asynchronous.
        /// </summary>
        /// <param name="userRoleId">The user role id.</param>
        /// <returns></returns>
        Task<AspNetUserRole> GetUserRoleByIdAsync(int userRoleId);
        Task InsertIntoUserHistoryAsync(UserHistory userHistory);
    }


    public class UserAccountManager : IUserAccountManager
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IIdentityManager _identityManager;
        public UserAccountManager(IUserAccountRepository userAccountRepository, IIdentityManager identityManager)
        {
            _userAccountRepository = userAccountRepository;
            _identityManager = identityManager;
        }
        public async Task<IEnumerable<AspNetUser>> GetUsersAsync()
            => await _userAccountRepository.GetAspNetUsersAsync();

        public async Task<bool> CreateUserAsync(AspNetUser aspnetuser)
            => await _userAccountRepository.PostAspNetUserAsync(aspnetuser);


        public async Task<bool> IsAValidLoginUserAsync(string username, string email)
            => await _userAccountRepository.GetAspNetUserAsync(username, email);

        /// <summary> GET / SELECT / the asp net user user asynchronous.</summary>
        /// <param name="email">The email used during registration.</param>
        /// <returns>AspNetUser</returns>
        public async Task<AspNetUser> GetUserByEmailAsync(string email)
            => await _userAccountRepository.GetAspNetUserAsync(email);


        /// <summary>
        /// Gets the asp net user user asynchronous.
        /// </summary>
        /// <param name="id">The user id .</param>
        /// <returns>aspnet user</returns>
        public async Task<AspNetUser> GetUserByIdAsync(int id)
            => await _userAccountRepository.GetAspNetUserByIdAsync(id);


        /// <summary>Determines whether [the specified email] IS ALREADY REGISTERED UNDER ASPNET USER.</summary>
        /// <param name="email">The registered email.</param>
        /// <returns></returns>
        public async Task<bool> IsAspNetUser(string email)
        {
            {
                var user = await _userAccountRepository.GetAspNetUserAsync(email);
                return user == null ? false : true;
            }
        }

        public async Task<bool> UpdateAspNetUserAsync(AspNetUser aspNetUser)
        {
            var user = await _userAccountRepository.UpdateAspNetUserAsync(aspNetUser);
            return user == null ? false : true;
        }


        /// <summary> GET / SELECT / the asp net user user asynchronous.</summary>
        /// <param name="email">The email used during registration.</param>
        /// <returns>AspNetUser</returns>
        public async Task<AspNetUser> GetUserAsync(string email)
            => await _userAccountRepository.GetAspNetUserAsync(email);

        public async Task<AspNetUser> FindUserAsync(int id)
            => await _userAccountRepository.FindAspNetUserAsync(id);

        public async Task InsertIntoUserLoginAttemptsAsync(UserLoginAttempt userLoginAttempt) => await _userAccountRepository.InsertIntoUserLoginAttemptsAsync(userLoginAttempt);
        public async Task<int> GetLoggedInUserId() => await _identityManager.GetLoggedInUserIdAsync();

        public Task<IEnumerable<UserRoles_Search_Result>> GetAllActiveUserRoles()
         => _userAccountRepository.GetActiveUserRoles();
        public async Task<AspNetUserRole> GetUserRoleByIdAsync(int userRoleId) => await _userAccountRepository.GetAspNetUserRolebyIdAsync(userRoleId);
        public async Task InsertIntoUserHistoryAsync(UserHistory userHistory) => await _userAccountRepository.PostUserHistoryAsync(userHistory);
    }

}
