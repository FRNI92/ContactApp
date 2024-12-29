// See https://aka.ms/new-console-template for more information
using ContactApp.Business.Interfaces;
using ContactApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((config, services) =>
    {
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IContactService, ContactService>();
        services.AddTransient<IContactFactory, IContactFactory>();

        //services.AddTransient<IMainMenuDialogs, MainMenuDialogs>();

    })
.Build();

//using var scope = host.Services.CreateScope();
//IMainMenuDialogs mainMenu = scope.ServiceProvider.GetRequiredService<IMainMenuDialogs>();

//mainMenu.RuntMenuOptions();