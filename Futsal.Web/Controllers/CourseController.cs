using Futsal.Services;
using System.Web.Mvc;

namespace Futsal.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private static IScheduleManager _scheduleManager;
        private static IIdentityManager _identityManager;
        public ScheduleController(IScheduleManager scheduleManager, IIdentityManager identityManager)
        {
            _scheduleManager = scheduleManager;
            _identityManager = identityManager;
        }



    }
}