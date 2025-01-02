using ContactApp.Business.Factories;
using ContactApp.Business.Interfaces;
using ContactApp.Business.Services;
using ContactApp_WPF.ViewModels;
using ContactApp_WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ContactApp_WPF
{
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((Context, services) =>
                {
                    services.AddSingleton<IFileService, FileService>();
                    services.AddSingleton<IContactService, ContactService>();
                    services.AddSingleton<IContactFactory, ContactFactory>();
                    services.AddTransient<StartPageViewModel>();
                    services.AddTransient<StartPageView>();
                    //services.AddTransient<SecondPageView>();
                    //services.AddTransient<SecondPageViewModel>();
                    //services.AddTransient<ThirdPageView>();
                    //services.AddTransient<ThirdPageViewModel>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

    }

}
