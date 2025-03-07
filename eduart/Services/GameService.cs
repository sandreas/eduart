using eduart.Models;

namespace eduart.Services;

public class GameService
{
    
    public Profile ActiveProfile { get; set; } = new();

    public void SwitchProfile(Profile profile)
    {
        ActiveProfile = profile;
    }
}