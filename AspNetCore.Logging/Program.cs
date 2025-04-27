using AspNetCore.Logging.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    public static void Main(string[] args)
    {
        using var serviceProvider = new ServiceCollection().AddLogging(config =>
        {
            // UserService logları için minimum seviye belirleniyor: sadece Warning ve üstü seviyeler görünsün
            config.AddFilter("AspNetCore.Logging.Users.UserService",LogLevel.Warning);  // Burada UserService logları için filter uygulanıyor
            config.AddFilter("Program", LogLevel.Warning);  // Program için de minimum Warning seviyesi
            config.AddConsole();
        }).AddScoped<UserService>()
        .BuildServiceProvider();
        
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();


        //logger.LogTrace("bu bir trace mesajıdır");
        //logger.LogDebug("bu bir debug mesajıdır");
        //logger.LogInformation("bu bir information mesajıdır");
        //logger.LogWarning("bu bir warning mesajıdır");
        //logger.LogError("bu bir Error mesajıdır");
        //logger.LogCritical("bu bir critical mesajıdır");


        //var userService = serviceProvider.GetRequiredService<UserService>();
        //userService.Register("Faruk");
        //userService.Register("Faruk");
        //userService.Register("Ahmet");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(@"C:\Users\Şevval\Source\Repos\SaidFaruk\Asp_NetCore_Practices\AspNetCore.Logging\Log\Logs.txt", rollingInterval: RollingInterval.Day,
            outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] Mesaj :  {Message}{NewLine}{Exception}")
            
            .CreateLogger();

        try
        {
            Log.Information("Uygulama başlatılıyor...");
            // Uygulamanın geri kalan kodları
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Uygulama başlatılırken bir hata oluştu");
        }
        finally
        {
            Log.CloseAndFlush(); // Logları düzgün şekilde kapat
        }
    }
}