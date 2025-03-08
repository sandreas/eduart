using System;
using System.IO;
using System.Linq;
using SoundFlow.Abstracts;
using SoundFlow.Components;
using SoundFlow.Providers;

namespace eduart.Services;

public class AudioService
{
    private readonly AudioEngine _engine;
    private readonly Mixer _mixer;
    private SoundPlayer? _backgroundPlayer;
    private SoundPlayer? _foregroundPlayer;

    public float BackgroundVolume
    {
        get => _backgroundPlayer?.Volume ?? 1f;
        set
        {
            if (_backgroundPlayer is null)
            {
                return;
            }
            _backgroundPlayer.Volume = value;
        }
    }

    public AudioService(AudioEngine engine, Mixer mixer)
    {
        _engine = engine;
        _mixer = mixer;
    }

    public void ChangeBackgroundMusic(Stream? stream, TimeSpan? loopStart=null, TimeSpan? loopEnd = null)
    {
        RemovePlayerComponentFromMixer(_backgroundPlayer);
        if (stream != null)
        {       
            // var provider = new ChunkedDataProvider(stream);
            var provider = new StreamDataProvider(stream);
            _backgroundPlayer = new SoundPlayer(provider)
            {
                IsLooping = true
            };
            
            
            /*
            var loopEndNormalized = loopEnd ?? TimeSpan.FromSeconds(-1);
            var loopStartNormalized = loopStart ?? TimeSpan.Zero;
            var loopStartSeconds = (float)loopStartNormalized.TotalMilliseconds / 1000;
            var loopEndSeconds = (float)loopEndNormalized.TotalMilliseconds / 1000;
            _backgroundPlayer.SetLoopPoints(loopStartSeconds, loopEndSeconds);
            */
            
            
            _mixer.AddComponent(_backgroundPlayer);
            _backgroundPlayer.SetLoopPoints(0);
            _backgroundPlayer.Play();
        }
    }

    public void PlaySound(Stream? stream)
    {
        RemovePlayerComponentFromMixer(_foregroundPlayer);
        if (stream == null)
        {
            return;
        }

        var provider = new ChunkedDataProvider(stream);
        _foregroundPlayer = new SoundPlayer(provider);

        _mixer.AddComponent(_foregroundPlayer);
        _foregroundPlayer.Play();
    }
    
    private void RemovePlayerComponentFromMixer(SoundPlayer? player)
    {
        if (player == null)
        {
            return;
        }
        player.Stop();
        _mixer.RemoveComponent(player);
    }
    
    ~AudioService()
    {
        foreach(var output in _mixer.Outputs)
        {
            _mixer.RemoveComponent(output);
        }
        
        _engine.Dispose();
    }
}