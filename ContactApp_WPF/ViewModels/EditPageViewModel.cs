using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactApp.Business.Interfaces;
using ContactApp.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace ContactApp_WPF.ViewModels;

public partial class EditPageViewModel : ObservableObject
{
    private readonly IContactService _contactService;
    private readonly IFileService _fileService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<Contact> _editContactList = new();

    [ObservableProperty]
    private Contact _selectedContact = new();
   
    [RelayCommand]
    private void GoToStartPage()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<StartPageViewModel>();

    }

    [RelayCommand]
    private void UpdateContact()
    {
        if(SelectedContact != null)
        {
            var updatedTrue = _contactService.Update(SelectedContact);
            if(!updatedTrue)
            {
                Console.WriteLine("Contact Not Updated");
            }
        }
    }

    
    [RelayCommand]
    private void DeleteContact() 
    {
        if (SelectedContact != null)
        {
            _contactService.Delete(SelectedContact.GuidId);
            EditContactList.Remove(SelectedContact);
        }
    }    

    public EditPageViewModel(IContactService contactService, IFileService fileService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _fileService = fileService;

        _serviceProvider = serviceProvider;
        var contactList = _fileService.LoadFromFile();
        EditContactList = new ObservableCollection<Contact>(contactList);
    }
}
