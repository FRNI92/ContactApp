using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp_WPF.ViewModels;

public partial class SecondPageViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;


    [ObservableProperty]
    private Contact _user = new();

    [RelayCommand]

    private void GoToStartPage()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<StartPageViewModel>();
    }
    [RelayCommand]
    private void SaveContact()
    {
        Contact contact = _contactService.Create();

        contact.FirstName = User.FirstName;
        contact.LastName = User.LastName;
        contact.Email = User.Email;
        contact.PhoneNumber = User.PhoneNumber;
        contact.StreetAdress = User.StreetAdress;
        contact.PostalCode = User.PostalCode;
        contact.City = User.City;

        _contactService.Add(contact);

    }
    public SecondPageViewModel(IServiceProvider serviceprovider, IContactService contactService, IFileService fileService)
    {
        _serviceProvider = serviceprovider;
        _contactService = contactService;
    }
}
