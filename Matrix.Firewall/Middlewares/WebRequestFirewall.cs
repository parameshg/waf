using Microsoft.Owin;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Matrix.Firewall.Middlewares
{
    public class WebRequestFirewall
    {
        private static Logger Log = LogManager.GetLogger("Matrix.Firewall");

        private Func<IDictionary<string, object>, Task> Next { get; set; }

        public WebRequestFirewall(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);

            var address = context.Request.RemoteIpAddress;

            Log.Debug<string>($"Evaluating access policy for ip address {context.Request.RemoteIpAddress}");

            if (IPAddress.Parse(address).AddressFamily.Equals(AddressFamily.InterNetworkV6))
            {
                Log.Trace<string>($"Converting ipv4 to ipv6 for {context.Request.RemoteIpAddress}...");

                address = IPAddress.Parse(address).MapToIPv4().ToString();

                Log.Trace<string>($"Converted ipv4 to ipv6: {context.Request.RemoteIpAddress} = {address}");
            }

            if (IPAddress.Parse(address).AddressFamily.Equals(AddressFamily.InterNetwork))
            {
                var api = Environment.GetEnvironmentVariable("matrix.firewall.api.url");

                Log.Trace<string>($"Environment's firewall is at {api}");

                if (!string.IsNullOrEmpty(api))
                {
                    Log.Trace<string>($"Accessing {api}/address/{address}/eval");

                    var json = new WebClient().DownloadString($"{api}/address/{address}/eval");

                    if (!string.IsNullOrEmpty(json))
                    {
                        var authorized = JsonConvert.DeserializeObject<bool>(json);

                        if (authorized)
                        {
                            Log.Debug<string>($"Access for ip address {context.Request.RemoteIpAddress} is authorized");

                            await Next.Invoke(environment);
                        }
                        else
                        {
                            Log.Debug<string>($"Access for ip address {context.Request.RemoteIpAddress} is unauthorized");

                            context.Response.StatusCode = 401;
                            context.Response.ReasonPhrase = "Unauthorized";
                        }
                    }
                    else
                    {
                        Log.Debug<string>($"Unable to evaluate access policy for ip address {context.Request.RemoteIpAddress}");

                        context.Response.StatusCode = 500;
                        context.Response.ReasonPhrase = "Internal Server Error";
                    }
                }
                else
                {
                    Log.Debug<string>($"Endpoint to evaluate access policy for ip address {context.Request.RemoteIpAddress} is unavailable");

                    context.Response.StatusCode = 500;
                    context.Response.ReasonPhrase = "Internal Server Error";
                }
            }
            else
            {
                Log.Debug<string>($"Unable to evaluate access policy for ip address {context.Request.RemoteIpAddress} due to its format");

                context.Response.StatusCode = 400;
                context.Response.ReasonPhrase = "Bad Request";
            }
        }
    }
}