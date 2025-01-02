using CommunityToolkit.Mvvm.ComponentModel;
using ContactApp.Business.Interfaces;

namespace ContactApp_WPF.ViewModels;

public partial class SecondPageViewModel : ObservableObject
{
    private readonly IServiceProvider _ServiceProvider;
    private readonly IContactService _contactService;
    private readonly IFileService _fileService;
}
