using System.Web.Mvc;

namespace Futsal.Web.Controllers
{
    //    [LogAction]
    public class ErrorController : Controller
    {
        //  GET: Error
        public ActionResult Index() => View();

        public ActionResult TooManyAttempts() => View();

        public ActionResult FileDoesntExist() => View();

        public ActionResult NCCIUserNotPermitted() => View();

        public ActionResult NotAspNetUser() => View();

        public ActionResult UserInactive() => View();

        public ActionResult PageDoesntExist() => View();
    }
}