using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Avalonia.SimplePreferences;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eduart.Services;

namespace eduart.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _content = null;

    [ObservableProperty]
    private bool _isLoading = true;
    public MainViewModel(HistoryRouter<ViewModelBase> router, ProfileService profiles)
    {
        router.CurrentViewModelChanged += viewModel => Content = viewModel;
        Task.Run(async () =>
        {
            IsLoading = true;
            await profiles.LoadAsync();
            IsLoading = false;
            router.GoTo<HomeViewModel>();
        });
    }
    
    
}
