using System.Collections.Concurrent;

namespace AspNetCore.Middleware.Middlewares.RateLimiting
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
       private readonly ConcurrentDictionary<string, RequestLog> _requestLog = new();
        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress?.ToString();
            if (remoteIp == null)
            {
                await _next(context); // Eğer IP yoksa bir sonraki middleware'e geç
                return;
            }

            var currentTime = DateTime.UtcNow;
            var requestLog = _requestLog.GetOrAdd(remoteIp, new RequestLog());

            // Son istek ile yeni gelen istek arasındaki zaman farkını kontrol ediyoruz
            if (currentTime - requestLog.LastRequestTime < TimeSpan.FromSeconds(1))
            {
                if (requestLog.RequestCount >= 5) // Eğer istek sayısı 5'ten fazla ise
                {
                    var timeToWait = TimeSpan.FromMinutes(1) - (currentTime - requestLog.LastRequestTime);
                    var timeToWaitSeconds = (int)timeToWait.TotalSeconds; // Kalan süreyi saniye olarak al

                    context.Response.StatusCode = 429; // "Too many requests" hatası
                    await context.Response.WriteAsync($"Too many requests, please try again in {timeToWaitSeconds} seconds.");
                    return; // Burada işlemi sonlandırıyoruz, middleware'e geçmiyoruz.
                }
            }
            else
            {
                if (currentTime - requestLog.LastRequestTime >= TimeSpan.FromMinutes(1))
                    requestLog.RequestCount = 0; // Eğer 1 dakika geçtiyse, sayacı sıfırlıyoruz
            }

            requestLog.RequestCount++;
            requestLog.LastRequestTime = currentTime;
            await _next(context); // Eğer rate limit aşılmadıysa, bir sonraki middleware'e geçiyoruz

        }

    }
}
