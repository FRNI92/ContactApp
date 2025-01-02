// See https://aka.ms/new-console-template for more information
using ContactApp.Business.Factories;
using ContactApp.Business.Interfaces;
using ContactApp.Business.Services;
using ContactApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((config, services) =>
    {
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IContactService, ContactService>();
        services.AddTransient<IContactFactory, ContactFactory>();

        services.AddTransient<IMenuDialogs, MenuDialogs>();

    })
.Build();

using var scope = host.Services.CreateScope();
IMenuDialogs menuDialogs = scope.ServiceProvider.GetRequiredService<IMenuDialogs>();

menuDialogs.RunMenuOptions();