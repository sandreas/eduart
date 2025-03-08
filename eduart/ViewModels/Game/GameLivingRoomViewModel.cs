using CommunityToolkit.Mvvm.ComponentModel;
using eduart.Models;
using eduart.Services;

namespace eduart.ViewModels.Game;

public partial class GameLivingRoomViewModel: ViewModelBase
{
    private readonly AudioService _audioService;
    private readonly AssetsService _assetsService;

    public GameLivingRoomViewModel(AudioService audioService, AssetsService assetsService)
    {
        _audioService = audioService;
        _assetsService = assetsService;
        _audioService.BackgroundVolume = 0.5f;
    }
}