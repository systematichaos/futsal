using Futsal.Data;
using Futsal.Entities;

namespace Futsal.Services
{
    public interface ILogManager
    {
        void InsertIntoTblApplicationlog(ApplicationLog log);
    }

    public class LogManager : ILogManager
    {
        private readonly ILogRepository _logRepository;

        public LogManager(ILogRepository logRepository) => _logRepository = logRepository;
        public void InsertIntoTblApplicationlog(ApplicationLog applicationLog) => _logRepository.PostApplicationLogAsync(applicationLog);
    }
}
