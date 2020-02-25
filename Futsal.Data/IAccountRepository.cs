using Futsal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

/*RELATED TO USER ROLES/ USERS IDENTITY STUFFS*/
namespace Futsal.Data
{

        public interface IIdentityRepository
    {
        Task<IEnumerable<AspNetRole>> GetAllRolesAsync();
        Task<bool> CreateRoleAsync(string roleName,int createdByUser);
        Task<AspNetRole> FindRolesAsync(int roleId);
       // Task<bool> DeleteRoleAsync(string id);
        Task<bool> UpdateRoleAsync(AspNetRole aspnetrole);
    }


    public class IdentityRepository : IIdentityRepository
    {
        public async Task<IEnumerable<AspNetRole>> GetAllRolesAsync()
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetRoles.ToListAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateRoleAsync(string roleName, int createdByUser)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    var aspnetrole = new AspNetRole() { Name = roleName,CreatedByUser=createdByUser,LastUpdatedByUser=createdByUser,CreatedDate=DateTime.Now,LastUpdatedDate=DateTime.Now};
                    db.AspNetRoles.Add(aspnetrole);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AspNetRole> FindRolesAsync(int roleId)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    return await db.AspNetRoles.FindAsync(roleId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRoleAsync(AspNetRole aspnetrole)
        {
            try
            {
                using (var db = new FutsalEntities())
                {
                    db.Entry(aspnetrole).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public async Task<bool> DeleteRoleAsync(string id)
        //{
        //    try
        //    {
        //        using (var db = new FutsalEntities())
        //        {
        //            var aspNetRole = await db.AspNetRoles.FindAsync(id);
        //            aspNetRole.IsDeleted = true;
        //           //  aspNetRole.LastUpdatedByUser = 
        //            aspNetRole.LastUpdatedDate = DateTime.Now;
        //            db.AspNetRoles.Remove(aspNetRole);
        //            db.Entry(aspNetRole).State = EntityState.Modified;
        //            await db.SaveChangesAsync();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

    }
}
