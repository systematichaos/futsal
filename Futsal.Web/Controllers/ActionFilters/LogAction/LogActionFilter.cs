using Futsal.Entities;
using Futsal.Services;
using System;
using System.Web;
using System.Web.Mvc;

namespace Futsal.Web.Controllers.ActionFilters.LogAction
{
    /// <summary>
    /// My custom ActionFilter for the LogActionAttribute. This is the class which gets called to complete the implementation of the attribute
    /// </summary>
    public class LogActionFilter : IActionFilter<LogActionAttribute>
    {
        private readonly ILogManager _logManager;
        private readonly IApplicationManager _applicationManager;
        private readonly IIdentityManager _identityManager;
        public LogActionFilter(ILogManager logManager, IApplicationManager applicationManager, IIdentityManager identityManager)
        {
            _logManager = logManager;
            _applicationManager = applicationManager;
            _identityManager = identityManager;
        }
        public async void OnActionExecuting(LogActionAttribute attribute, ActionExecutingContext context)
        {
            //Only log authenticated requests
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //If the SessionInfo.AppID is null(when it hasn't been set yet on login) then call a method to get it
                //To avoid deadlocks with running an async method synchronously, I decided to just create a sync method of it. I know its a code smell but I thought that was the best solution(its only one tiny method)

                _logManager.InsertIntoTblApplicationlog(
                    new ApplicationLog
                    {
                        ApplicationId = await _applicationManager.ReturnApplicationIDAsync(),
                        Controller = context.RouteData.Values["Controller"].ToString(),
                        Action = context.RouteData.Values["Action"].ToString(),
                        IPAddress = _applicationManager.ReturnIPAddress(),
                        UserAgent = HttpContext.Current.Request.UserAgent,
                        UserId = await _identityManager.GetLoggedInUserIdAsync(),
                        LogDateTime = DateTime.Now,
                        Username = _identityManager.GetLoggedInUserName(),
                        SessionId = ""
                    }

                     );
            }
        }
    }

}