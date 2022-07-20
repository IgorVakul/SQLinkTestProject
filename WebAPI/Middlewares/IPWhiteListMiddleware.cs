using Microsoft.Extensions.Options;
using System.Net;

namespace WebAPI.Middlewares
{

    public class IPWhiteListOptions
    {
        public List<string> Whitelist { get; set; }
    }

    public class IPWhiteListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IPWhiteListMiddleware> _logger;
        private readonly IPWhiteListOptions _iPWhitelistOptions;       

        public IPWhiteListMiddleware(
            RequestDelegate next,
            ILogger<IPWhiteListMiddleware> logger,
            IOptions<IPWhiteListOptions> applicationOptionsAccessor)
        {
            _iPWhitelistOptions= applicationOptionsAccessor.Value;
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethod.Get.Method)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                _logger.LogDebug("Request from Remote IP address: {RemoteIp}", remoteIp);              

                var isIPWhitelisted = _iPWhitelistOptions.Whitelist
                    .Where(ip => IPAddress.Parse(ip)
                    .Equals(remoteIp))
                    .Any();                

                if (!isIPWhitelisted)
                {
                    _logger.LogWarning(
                        "Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }

    public static class IPWhiteListMiddlewareExtensions
    {
        /// <summary>
        /// UseCustomExceptions() - Uses a middleware that handles exception thrown in code, and converts them to appropriate HTTP responses
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseIPWhiteListMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPWhiteListMiddleware>();
        }
    }
}
