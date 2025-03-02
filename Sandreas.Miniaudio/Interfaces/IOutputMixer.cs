namespace Sandreas.Miniaudio.Interfaces;

public interface IOutputMixer
{
    public void MixOutput(Span<float> buffer);
}