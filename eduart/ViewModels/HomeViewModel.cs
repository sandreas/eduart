using Avalonia.SimplePreferences;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using eduart.Models;
using eduart.Services;


namespace eduart.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly PreferencesService _prefs;
    private readonly ProfileService _profiles;
    private readonly HistoryRouter<ViewModelBase> _router;


    public HomeViewModel(HistoryRouter<ViewModelBase> router, ProfileService profiles)
    {
        _router = router;
        _profiles = profiles;
    }

    [RelayCommand]
    public void AddProfile()
    {
        var vm = _router.GoTo<EditProfileViewModel>();
        vm.Profile = new Profile();
    }
    
    [RelayCommand]
    public void EditProfile(Profile profile)
    {
        var vm = _router.GoTo<EditProfileViewModel>();
        vm.Profile = profile;
    }

    [RelayCommand]
    public void SelectProfile(Profile profile)
    {
        
    }

    [RelayCommand]
    public void DeleteProfile(Profile profile)
    {
        
    }
    
}