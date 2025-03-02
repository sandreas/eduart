using System;
using System.IO;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Enums;
using SoundFlow.Providers;


namespace eduart.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    // private readonly PreferencesService _preferences;

    // private MediaPlayer _mediaPlayer;

    [ObservableProperty] private TimeSpan _time;

    
    
    public HomeViewModel(/*PreferencesService prefs*/)
    {
        // _preferences = prefs;
        /*
        var libVlc = new LibVLC(enableDebugLogs: true);
        _mediaPlayer = new MediaPlayer(libVlc);
        _mediaPlayer.TimeChanged += OnTimeChanged;
        _mediaPlayer.Media = new Media(libVlc, new Uri("https://download.samplelib.com/mp3/sample-15s.mp3"));
        */
        
        
        /*
        var miniAudio = new MiniAudioAdapter(AudioAdapter.DefaultSampleRate, false, AudioGroup.Default);
        miniAudio.Initialize();
        */
        
        // var path = "https://download.samplelib.com/mp3/sample-15s.mp3";
        var path = "/home/andreas/Musik/sample-15s.mp3";
        // AudioSource currentSource = null;
        // Stop previous source

        // Create new source and play
        // AudioAdapter.Instance =  new MiniAudioAdapter(44100, false, group);
        /*
        AudioAdapter.Instance = new MiniAudioAdapter(44100, false, AudioGroup.Default);

        var stream = File.Open(path, FileMode.Open);
        var currentSource = new AudioSource(stream) { IsLooping = true };
        // currentSource?.Stop();

        currentSource.Play();
        */
        
        // Initialize the audio engine with the MiniAudio backend
        using var audioEngine = new MiniAudioEngine(44100, Capability.Playback);

// Create a SoundPlayer and load an audio file
        var player = new SoundPlayer(new StreamDataProvider(File.OpenRead(path)));

// Add the player to the master mixer
        Mixer.Master.AddComponent(player);

// Start playback
        player.Play();
        Thread.Sleep(3000);
    }

}