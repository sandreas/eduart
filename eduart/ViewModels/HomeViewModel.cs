using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.SimplePreferences;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eduart.Models;
using eduart.Services;
using eduart.ViewModels.Game;


namespace eduart.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly HistoryRouter<ViewModelBase> _router;
    private readonly ProfileService _profileService;
    private readonly GameService _gameService;
    private readonly AudioService _audioService;
    private readonly AssetsService _assetsService;


    [ObservableProperty]
    private Profile? _selectedProfile;


    public List<Profile> Profiles => _profileService.Profiles;


    
    public HomeViewModel(HistoryRouter<ViewModelBase> router, ProfileService profileService, GameService gameService, AudioService audioService, AssetsService assetsService)
    {
        _router = router;
        _profileService = profileService;
        _gameService = gameService;
        _audioService = audioService;
        _assetsService = assetsService;

        _audioService.ChangeBackgroundMusic(_assetsService.Open("/Assets/audio/background-music/game-music-loop-6-144641.mp3"));
        
    }
    
    
    partial void OnSelectedProfileChanged(Profile? value)
    {
        if (value != null)
        {
            _gameService.SwitchProfile(value);
            _router.GoTo<GameMainViewModel>();
            _audioService.PlaySound(_assetsService.Open("/Assets/audio/sounds/game-start-6104.mp3"));
        }
    }


    [RelayCommand]
    private void AddProfile()
    {
        var vm = _router.GoTo<EditProfileViewModel>();
        vm.Profile = new Profile();
    }
    
    [RelayCommand]
    private void EditProfile(Profile profile)
    {
        var vm = _router.GoTo<EditProfileViewModel>();
        vm.Profile = profile;
    }


    [RelayCommand]
    private void DeleteProfile(Profile profile)
    {
        // Todo idea: 
        // var confirm = _router.Goto<ConfirmationViewModel>();
        // confirm.DeleteHandler("Möchten Sie dem Gerät wirklich löschen?", () => {
        //  _profileService.Remove(profile.Id);
        //  _profileService.SaveAsync();
        // }
        //
        
    }
    
}