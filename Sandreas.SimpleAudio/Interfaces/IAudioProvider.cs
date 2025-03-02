namespace Sandreas.SimpleAudio.Interfaces;

internal interface IAudioProvider
{
    int Position { get; }

    int Length { get; }

    bool CanSeek { get; }

    int ReadSamples(Span<short> samples);

    void Seek(int offset);
}