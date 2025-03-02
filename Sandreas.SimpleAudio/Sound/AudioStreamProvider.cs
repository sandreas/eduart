using Sandreas.Miniaudio.Adapters;
using Sandreas.Miniaudio.Interfaces;
using Sandreas.SimpleAudio.Interfaces;

namespace Sandreas.SimpleAudio.Sound;

public class AudioStreamProvider : IAudioProvider
{
    private readonly IAudioDecoder _decoder;
    private readonly Stream _stream;

    public AudioStreamProvider(Stream stream)
    {
        _decoder = AudioAdapter.Instance.CreateDecoder(stream);
        _stream = stream;
    }

    public int Length => _decoder.Length;

    public bool CanSeek => _stream.CanSeek;

    public int Position { get; private set; }

    public int ReadSamples(Span<short> samples)
    {
        // todo [sandreas]: ulong vs int
        var count = (int)_decoder.Decode(samples);
        Position += count;
        return count;
    }

    public void Seek(int offset)
    {
        // todo [sandreas]: ulong vs int
        _decoder.Seek((ulong)offset);
        Position = offset;
    }
}