using Futsal.Entities;

namespace Futsal.Data
{
    public interface ILogRepository
    {
        void PostApplicationLogAsync(ApplicationLog applicationLog);
    }
    public class LogRepository : ILogRepository
    {
        public void PostApplicationLogAsync(ApplicationLog applicationLog)
        {
            using (var dbContext = new FutsalEntities())
            {
                dbContext.ApplicationLogs.Add(applicationLog);
                dbContext.SaveChanges();
            }
        }


    }
}
