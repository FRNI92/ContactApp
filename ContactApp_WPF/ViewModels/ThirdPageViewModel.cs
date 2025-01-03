
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace ContactApp_WPF.ViewModels;

public partial class ThirdPageViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFileService _fileService;


    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = new();

    [RelayCommand]
    private void GoToSecondPage()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<SecondPageViewModel>();
    }



    public ThirdPageViewModel(IServiceProvider serviceProvider, IFileService fileService)
    {
        _serviceProvider = serviceProvider;
        _fileService = fileService;

        var contactList = _fileService.LoadFromFile();
        Contacts = new ObservableCollection<Contact>(contactList);
    }
}
