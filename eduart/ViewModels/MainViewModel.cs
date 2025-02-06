using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace eduart.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _content = null;

    public MainViewModel(HistoryRouter<ViewModelBase> router)
    {
        router.CurrentViewModelChanged += viewModel => Content = viewModel;
        router.GoTo<HomeViewModel>();
    }
    
    
}
