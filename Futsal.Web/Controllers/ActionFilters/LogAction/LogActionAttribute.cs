using System.Web.Mvc;

namespace Futsal.Web.Controllers.ActionFilters.LogAction
{
    /// <summary>
    /// This is my custom ActionFilterAttribute. It is passive(contains no code) as the implementation will be taken care of in my LogActionFilter
    /// For more info look here: https://cuttingedge.it/blogs/steven/pivot/entry.php?id=98
    /// We need to use DI correctly, and to do this I have had to follow the above article. We are required to create the abstract way of injected into attributes because 
    /// the CLR creates the Attribute, not the framework so we cannot detect when it has been created and inject our dependencies. This is why we must create an ActionFilter
    /// as this is managed by the framework and we can detect when this is created and therefore inject our dependencies later. There are also reasons to why we shouldn't use
    /// property injection or to just get the instance which we require. Look here for more info: https://github.com/simpleinjector/SimpleInjector/issues/18
    /// 
    /// I did make a stack overflow post for this and here is the result: https://stackoverflow.com/questions/50243550/simple-injector-mvc-actionfilter-dependency-injection
    /// </summary>
    public class LogActionAttribute : ActionFilterAttribute
    {
    }
}