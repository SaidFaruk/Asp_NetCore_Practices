using System.Net;

namespace AspNetCore.Middleware.Middlewares.IpRestriction
{
    public class IpRestrictionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _allowedIps;

        public IpRestrictionMiddleware(RequestDelegate next, List<string> allowedIps)
        {
            _next = next;
            _allowedIps = allowedIps;
        }

        public async Task Invoke(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            if (remoteIp != null && !_allowedIps.Contains(remoteIp))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Access Denied : Your IP is not allowed");
                return;
            }

            await _next(context);
        }
    }
}