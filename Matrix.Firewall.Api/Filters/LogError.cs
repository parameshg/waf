using NLog;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Matrix.Firewall.Api.Filters
{
    public class LogError : ExceptionFilterAttribute, IExceptionFilter
    {
        private static Logger Log = LogManager.GetLogger("Matrix.Firewall.Api");

        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext context, CancellationToken cancel)
        {
            await Task.Run(() => 
            {
                Log.Error<string>(context.Exception.ToString());
            });
        }
    }
}