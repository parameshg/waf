using NLog;
using System.Web.Mvc;

namespace Matrix.Firewall.Web.Filters
{
    public class LogRequest : FilterAttribute, IActionFilter
    {
        private static Logger Log = LogManager.GetLogger("Matrix.Firewall.Web");

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Info<string>(string.Format("[{0} | {1}] {2}", context.HttpContext.Request.Headers["REMOTE_ADDR"], context.HttpContext.Request.HttpMethod, context.HttpContext.Request.Url.ToString()));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}