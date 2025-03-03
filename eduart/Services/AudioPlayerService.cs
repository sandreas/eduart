using System;
using System.IO;
using SoundFlow.Abstracts;
using SoundFlow.Components;
using SoundFlow.Interfaces;
using SoundFlow.Providers;

namespace eduart.Services;

public class AudioPlayerService
{
    private readonly AudioEngine _engine;
    private readonly Mixer _mixer;

    private ISoundDataProvider? _dataProvider;
    private SoundPlayer? _player;
    private string _currentSongUrl = "";

    public AudioPlayerService(AudioEngine engine, Mixer mixer)
    {
        _engine = engine;
        _mixer = mixer;
    }
    public void LoadUri(Uri uri) => LoadOnChange(uri.ToString().Trim(), s => new NetworkDataProvider(s));

    private void LoadOnChange(string songUrl, Func<string, ISoundDataProvider> providerCallback)
    {
        if (_currentSongUrl == songUrl)
        {
            return;
        }

        _currentSongUrl = songUrl;
        ReloadDataProvider(providerCallback(songUrl));
    }
    private void ReloadDataProvider(ISoundDataProvider networkDataProvider)
    {
        RemovePlayerComponentFromMixer();
        _dataProvider = networkDataProvider;
        _player = new SoundPlayer(_dataProvider);
        _mixer.AddComponent(_player);
    }

    private void RemovePlayerComponentFromMixer()
    {
        if (_player == null)
        {
            return;
        }
        _player.Stop();
        _mixer.RemoveComponent(_player);
        _player = null;
        DisposeDataProviderIfExists();
    }

    public void LoadFile(string path) => LoadOnChange(path, u => new ChunkedDataProvider(u));

    public void Play() => _player?.Play();
    public void Pause() => _player?.Pause();
    public void Stop() => _player?.Stop();

    public void SeekRelative(TimeSpan offset) => Seek(offset, SeekOrigin.Current);
    
    public void Seek(TimeSpan to, SeekOrigin seekOrigin = SeekOrigin.Begin)
    {
        var seekOffset = (float)to.TotalMilliseconds / 1000;
        switch (seekOrigin)
        {
            case SeekOrigin.Current:
                _player?.Seek(_player.Time + seekOffset);
                break;
            case SeekOrigin.End:
                _player?.Seek(_player.Duration + seekOffset);
                break;
            case SeekOrigin.Begin:
            default:
                _player?.Seek(seekOffset);
                break;
        }
    }

    private void DisposeDataProviderIfExists()
    {
        /*
        if (_dataProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
        */
        _dataProvider = null;
    }

    ~AudioPlayerService()
    {
        DisposeDataProviderIfExists();
        _engine.Dispose();
    }
}