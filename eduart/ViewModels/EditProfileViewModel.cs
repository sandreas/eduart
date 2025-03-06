using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eduart.Models;
using eduart.Services;

namespace eduart.ViewModels;

public partial class EditProfileViewModel: ViewModelBase
{
    [ObservableProperty] private Profile _profile = new();

    [ObservableProperty] private string _maxPlayTime = "45";
    [ObservableProperty] private string _playerName = "";
    [ObservableProperty] private string _npcName = "";

    
    private readonly ProfileService _profileService;
    private readonly HistoryRouter<ViewModelBase> _router;

    public EditProfileViewModel(HistoryRouter<ViewModelBase> router, ProfileService profileService)
    {
        _profileService = profileService;
        _router = router;
    }

    partial void OnProfileChanged(Profile value)
    {
        MaxPlayTime = value.MaxPlayTime.TotalMinutes.ToString(CultureInfo.InvariantCulture);
        PlayerName = value.Characters.FirstOrDefault(c => c.Type == CharacterType.Player)?.Name ?? "";
        NpcName = value.Characters.FirstOrDefault(c => c.Type == CharacterType.Npc)?.Name ?? "";
    }
    
    [RelayCommand]
    public void Save()
    {
        if (int.TryParse(MaxPlayTime, out var maxPlayTime))
        {
            Profile.MaxPlayTime = TimeSpan.FromMinutes(maxPlayTime);
        }

        CreateOrUpdateCharacter(CharacterType.Player, PlayerName);
        CreateOrUpdateCharacter(CharacterType.Player, PlayerName);

        if (!_profileService.Profiles.Contains(Profile))
        {
            _profileService.Profiles.Add(Profile);
        }

        
        Task.Run(async () =>
        {
            await _profileService.PersistAsync();
            _router.GoTo<HomeViewModel>();
        });
    }

    private void CreateOrUpdateCharacter( CharacterType type, string name)
    {
        var character = Profile.Characters.FirstOrDefault(c => c.Type == type) ?? new Character()
        {
            Type = CharacterType.Player,
            Name = PlayerName
        };

        if (!Profile.Characters.Contains(character))
        {
            Profile.Characters.Add(character);
        }
    }
    
}