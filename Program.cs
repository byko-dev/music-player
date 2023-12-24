using Avalonia;
using System;
using Microsoft.Extensions.DependencyInjection;
using music_player.Database;
using music_player.Repository;

namespace music_player;

class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }
    
    [STAThread]
    public static void Main(string[] args)
    {
        LoadDependencyInjectionServicesContext();
        
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static void LoadDependencyInjectionServicesContext()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }
    
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();    
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DatabaseConnectionContext>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
