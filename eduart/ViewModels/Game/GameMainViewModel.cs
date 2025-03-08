using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eduart.Models;
using eduart.Services;

namespace eduart.ViewModels.Game;

public partial class GameMainViewModel: ViewModelBase
{
    private readonly AudioService _audioService;
    private readonly AssetsService _assetsService;
    private readonly HistoryRouter<ViewModelBase> _router;

    public GameMainViewModel(HistoryRouter<ViewModelBase> router, AudioService audioService, AssetsService assetsService)
    {
        _audioService = audioService;
        _assetsService = assetsService;
        _audioService.ChangeBackgroundMusic(_assetsService.Open("/Assets/audio/background-music/forest-ambience-296528.mp3"));
        _router = router;
    }

    [RelayCommand]
    private void EnterHouse()
    {
        _router.GoTo<GameLivingRoomViewModel>();
    }
}