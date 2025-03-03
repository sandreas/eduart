using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eduart.Services;


namespace eduart.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    // private readonly PreferencesService _preferences;

    // private MediaPlayer _mediaPlayer;

    [ObservableProperty] private TimeSpan _time;
    [ObservableProperty] private string _songUrl = "";

    private readonly AudioPlayerService _player;


    public HomeViewModel(AudioPlayerService player /*PreferencesService prefs*/)
    {
        _player = player;
        // https://dss-kiel.de/images/media_center/signals/lombard/male_0_kmh.mp3
        // var path = "https://download.samplelib.com/mp3/sample-15s.mp3";
        
    }

    partial void OnSongUrlChanged(string value)
    {
        var builder = new UriBuilder(SongUrl.Trim());
        _player.LoadUri(builder.Uri);
    }

    [RelayCommand]
    public void ChangeSong(string songNumber)
    {
        switch (songNumber)
        {
            case "song1":
                SongUrl = "http://commondatastorage.googleapis.com/codeskulptor-demos/DDR_assets/Kangaroo_MusiQue_-_The_Neverwritten_Role_Playing_Game.mp3";
                break;
            case "song2":
                SongUrl = "http://commondatastorage.googleapis.com/codeskulptor-demos/DDR_assets/Sevish_-__nbsp_.mp3";
                break;
        }
    }

    [RelayCommand]
    public void PlayerAction(string action)
    {
        switch (action)
        {
            case "Play":
                _player.Play();
                break;
            case "Pause":
                _player.Pause();
                break;
            case "StepBack":
                _player.SeekRelative(TimeSpan.FromSeconds(-30));
                break;
            case "StepForward":
                _player.SeekRelative(TimeSpan.FromSeconds(30));
                break;
        }   
    }



}