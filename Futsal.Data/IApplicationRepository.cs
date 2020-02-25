using Futsal.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Futsal.Data
{
    public interface IApplicationRepository
    {
        /// <summary>
        /// Gets the application ID asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> GetApplicationIdAsync();

        /// <summary>
        /// Gets the application name asynchronously
        /// </summary>
        /// <returns></returns>
        Task<string> GetApplicationNameAsync();
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public async Task<int> GetApplicationIdAsync()
        {
            try
            {
                using (var dbContext = new FutsalEntities())
                {
                    return (await dbContext.Applications.Where(x => x.Name == Constants.ApplicationName).SingleAsync()).Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Gets the application name asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetApplicationNameAsync()
        {
            try
            {
                using (var dbContext = new FutsalEntities())
                {
                    return (await dbContext.Applications.Where(x => x.Name == Constants.ApplicationName).SingleAsync()).Name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
