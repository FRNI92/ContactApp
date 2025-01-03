
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp_WPF.ViewModels;

public partial class StartPageViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;


    [RelayCommand]
    private void GoToSecondPage()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<SecondPageViewModel>();

    }
    [RelayCommand]
    private void GoToThirdPage()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ThirdPageViewModel>();

    }    
    
    [RelayCommand]
    private void GoToEditPage()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<EditPageViewModel>();

    }

    public StartPageViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}
