using AspNetCore.Middleware.Middlewares.IpRestriction;
using AspNetCore.Middleware.Middlewares.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.NoContent());

var allowedIps = builder.Configuration.GetSection("AllowedIPs").Get<List<string>>();

// Rate limiting ve IP kýsýtlamasýný sýrasýyla ekliyoruz
app.UseMiddleware<IpRestrictionMiddleware>(allowedIps);
app.UseMiddleware<RateLimitingMiddleware>();
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello, your IP address is allowed access!");
});


app.Run();





