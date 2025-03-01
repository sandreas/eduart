namespace Sandreas.Miniaudio.Decoders;

public class MiniAudioDecoderConfig
{
    public uint Channels { get; set; } = 2;
    public uint SampleRate { get; set; } = (uint)Sandreas.Miniaudio.NativeWrappers.Enums.SampleRate.RATE_44100;
}