using Futsal.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Futsal.Services
{
    public interface IScheduleManager
    {
        Task<IEnumerable<string>> GetAvailableSchedulesAsync();
    }


    public class ScheduleManager : IScheduleManager
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleManager(IScheduleRepository scheduleRepository) => _scheduleRepository = scheduleRepository;

        public Task<IEnumerable<string>>  GetAvailableSchedulesAsync() => throw new System.NotImplementedException();
    }
}
