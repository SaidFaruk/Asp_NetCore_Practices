// See https://aka.ms/new-console-template for more information


using AspNetCore.DependencyInjection.Controller;
using AspNetCore.DependencyInjection.Interfaces;
using AspNetCore.DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    //services.AddScoped<IMyService, MyService>();
    //services.AddSingleton<IMyService,MyService>();
    services.AddTransient<IMyService,MyService>();  
    services.AddTransient<MyController>();
}).Build();

Console.WriteLine("=== DI Test Started ===");

var service1 = host.Services.GetRequiredService<MyController>();
service1.Run();

var service2 = host.Services.GetRequiredService<MyController>();
service2.Run();

Console.WriteLine("=== DI Test Finished ===");