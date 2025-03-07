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
    // private readonly PreferencesService _prefs;
    private readonly ProfileService _profileService;
    private readonly HistoryRouter<ViewModelBase> _router;

    [ObservableProperty]
    private Profile? _selectedProfile;
    
    public List<Profile> Profiles => _profileService.Profiles;


    partial void OnSelectedProfileChanged(Profile? value)
    {
        if (value != null)
        {
            // todo idea: _gameService.Start(value);
            var vm = _router.GoTo<GameMainViewModel>();
            vm.Profile = value;
        }
    }
    
    
    public HomeViewModel(HistoryRouter<ViewModelBase> router, ProfileService profileService)
    {
        _router = router;
        _profileService = profileService;
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