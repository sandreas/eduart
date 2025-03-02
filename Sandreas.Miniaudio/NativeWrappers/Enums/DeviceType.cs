namespace Sandreas.Miniaudio.NativeWrappers.Enums;

[Flags]
internal enum DeviceType
{
    Playback = 0b00000001,
    Capture = 0b00000010,
    Duplex = Playback | Capture,
    Loopback = 0b00000100, // 1 << 3 
}