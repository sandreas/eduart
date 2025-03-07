using CommunityToolkit.Mvvm.ComponentModel;
using eduart.Models;
using eduart.Services;

namespace eduart.ViewModels.Game;

public partial class GameMainViewModel: ViewModelBase
{
    private readonly AudioService _audioService;
    private readonly AssetsService _assetsService;

    public GameMainViewModel(AudioService audioService, AssetsService assetsService)
    {
        _audioService = audioService;
        _assetsService = assetsService;
        _audioService.ChangeBackgroundMusic(_assetsService.Open("/Assets/audio/background-music/forest-163012.mp3"));

    }
}