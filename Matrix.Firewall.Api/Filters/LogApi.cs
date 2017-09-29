using NLog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Matrix.Firewall.Api.Filters
{
    public class LogApi : FilterAttribute, IActionFilter
    {
        private static Logger Log = LogManager.GetLogger("Matrix.Firewall.Api");

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext context, CancellationToken token, Func<Task<HttpResponseMessage>> continuation)
        {
            Log.Info<string>(string.Format("[{0} | {1}] {2}", context.Request.Headers.Host, context.Request.Method, context.Request.RequestUri.ToString()));

            if (!token.IsCancellationRequested)
                return await continuation.Invoke();
            else
                return null;
        }
    }
}