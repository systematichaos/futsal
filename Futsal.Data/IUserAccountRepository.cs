using Futsal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;


namespace Futsal.Data
{
    public interface IUserAccountRepository
    {
        Task<IEnumerable<AspNetUser>> GetAspNetUsersAsync();
        Task<bool> PostAspNetUserAsync(AspNetUser aspnetuser);
        Task<bool> GetAspNetUserAsync(string username, string email);
        Task<AspNetUser> GetAspNetUserAsync(string email);
        Task<IEnumerable<AspNetUser>> GetActiveAspNetUsersAsync();
        IEnumerable<AspNetUser> GetActiveAspNetUsers();



        /// <summary>
        /// Gets the ASP net user by user id address asynchronous.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>AspNetUser</returns>
        Task<AspNetUser> GetAspNetUserByIdAsync(int id);

        /// <summary>
        /// Updates the editted Asp net user.
        /// </summary>
        /// <param name="aspNetUser">The ASP net user.</param>
        /// <returns>updated aspnetuser</returns>
        Task<AspNetUser> UpdateAspNetUserAsync(AspNetUser aspNetUser);
        /// <summary>
        /// Updates the editted Asp net user.
        /// </summary>
        /// <param name="aspNetUser">The ASP net user.</param>
        /// <returns>updated true or false</returns>
        Task<bool> UpdateAsync(AspNetUser aspNetUser);
        Task<AspNetUser> FindAspNetUserAsync(int id);
        Task<bool> DeleteAspNetUserAsync(int id);
        bool AssignRoleToUser(int userId, string role, int loggedInUserId);
        Task InsertIntoUserLoginAttemptsAsync(UserLoginAttempt userLoginAttempt);
        Task<int> GetAspNetUserByIdAsync(string loggedInUser);
        Task<IEnumerable<UserRoles_Search_Result>> GetActiveUserRoles();
        Task<AspNetUserRole> GetAspNetUserRolebyIdAsync(int userRoleId);
        Task PostUserHistoryAsync(UserHistory userHistory);
    }

    public class UserAccountRepository : IUserAccountRepository
    {
        public async Task<IEnumerable<AspNetUser>> GetAspNetUsersAsync()
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetUsers.ToListAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<AspNetUser>> GetActiveAspNetUsersAsync()
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetUsers.Where(p => p.IsUserActive == true && p.IsDeleted == false).ToListAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<AspNetUser> GetActiveAspNetUsers()
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return db.AspNetUsers.Where(p => p.IsUserActive == true && p.IsDeleted == false).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PostAspNetUserAsync(AspNetUser aspnetuser)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    db.AspNetUsers.Add(aspnetuser);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validate the username and email association 
        /// Does the username with the email addres exist?
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns>true if the username email association is found. False , if not found</returns>
        public async Task<bool> GetAspNetUserAsync(string username, string email)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    var user = await db.AspNetUsers.Where(p => p.UserName == username && p.Email == email).SingleOrDefaultAsync();
                    return user == null ? false : true;
                }
            }
            catch (Exception ex)
            {
                var sth = ex;
                // throw ex;
                //log error here
                return false;
            }
        }

        /// <summary>
        /// Gets the ASP net user by email address asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>AspNetUser</returns>
        public async Task<AspNetUser> GetAspNetUserAsync(string email)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetUsers.Where(p => p.Email == email).SingleOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the ASP net user by user id address asynchronous.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns> AspNetUser</returns>
        public async Task<AspNetUser> GetAspNetUserByIdAsync(int id)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetUsers.Where(p => p.Id == id).SingleOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(AspNetUser aspNetUser)
        {
            try
            {
                var aspnetuser = await UpdateAspNetUserAsync(aspNetUser);
                return aspNetUser == null ? false : true;
            }
            catch (Exception ex)
            {
                var sth = ex;//log error here               
            }
            return false;
        }

        /// <summary>
        /// Updates the editted Aspnet user.
        /// </summary>
        /// <param name="aspNetUser">The ASP net user.</param>
        /// <returns>updated aspnet user</returns>
        public async Task<AspNetUser> UpdateAspNetUserAsync(AspNetUser aspNetUser)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    db.Entry(aspNetUser).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var sth = ex;//log error here               
            }
            return aspNetUser;
        }

        /// <summary>
        /// Finds the ASP net user asynchronously by aspnet user id.
        /// </summary>
        /// <param name="id">The Aspnet user ID.</param>
        /// <returns>aspnet user </returns>
        public async Task<AspNetUser> FindAspNetUserAsync(int id)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetUsers.FindAsync(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> DeleteAspNetUserAsync(int id)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    var aspNetUser = await db.AspNetUsers.FindAsync(id);
                    if (aspNetUser != null)
                    {
                        aspNetUser.IsDeleted = true;
                        aspNetUser.LastUpdatedByUser = 1;
                        aspNetUser.LastUpdatedDate = DateTime.Now;

                        db.Entry(aspNetUser).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool AssignRoleToUser(int userId, string role, int loggedInUserId)
        {
            try
            {
                var result = new ObjectParameter("Result", typeof(bool));
                using (var db = new FutsalEntities())
                {
                    int response = db.UserRole_Assign(userId, role, loggedInUserId, result);
                    return !string.IsNullOrEmpty(result.Value.ToString()) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task InsertIntoUserLoginAttemptsAsync(UserLoginAttempt userLoginAttempt)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    db.UserLoginAttempts.Add(userLoginAttempt);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> GetAspNetUserByIdAsync(string loggedInUser)
        {
            using (var db = new FutsalEntities())
            {
                var users = await db.AspNetUsers.Where(p => p.UserName == loggedInUser).SingleAsync();
                return users == null ? 0 : users.Id;
            }
        }

        public async Task<IEnumerable<UserRoles_Search_Result>> GetActiveUserRoles()
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return db.UserRoles_Search(null,null,null, null,0,25, null).ToList();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<AspNetUserRole> GetAspNetUserRolebyIdAsync(int userRoleId)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return db.AspNetUserRoles.Where(p => p.Id == userRoleId && p.IsDeleted == false).SingleAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task PostUserHistoryAsync(UserHistory userHistory)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    db.UserHistories.Add(userHistory);
                    await db.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}


/*
   {
            try
            {
                using (var db = new FutsalEntities())
                {
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
     
     
     
     
     */
