namespace AspNetCore.Middleware.Middlewares.RateLimiting
{
    public class RequestLog
    {
        public int RequestCount { get; set; }
        public DateTime LastRequestTime { get; set; } =DateTime.UtcNow;
    }
}
