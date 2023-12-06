using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using NLog.Extensions.Logging;
using System.Threading;
using System.Windows;
using Tools.Helpers;
using Tools.ViewModels;
using Tools.Views;

namespace Tools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //单实例启动
            SingleInstanceHelper.Check();
   
            var host = Host.CreateDefaultBuilder()
                     .ConfigureAppConfiguration((context, builder) =>
                     {
                         // Add other configuration files...
                         builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                     })
                     .ConfigureServices((context, services) =>
                     {
                         ConfigureServices(context.Configuration, services);
                     })
                     .ConfigureLogging(logging =>
                     {
                         logging.AddNLog();
                     })
                     .Build();
            Ioc.Default.ConfigureServices(host.Services); 
        }

        static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            //配置
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<QrcodeViewModel>();
            services.AddSingleton<SunflowerViewModel>();
            services.AddSingleton<AppSettings>();
            services.AddSingleton<CommandsViewModel>();
            services.AddSingleton<Commands>();
        }
    }
}
