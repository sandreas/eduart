using System;
using System.Collections.Generic;
using System.Linq;

namespace eduart.Models;

public class Profile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Image { get; set; } = string.Empty;
    public TimeSpan MaxPlayTime { get; set; } = TimeSpan.FromMinutes(45);
    public List<Character> Characters { get; set; } = [];
    public List<Achievement> Achievements { get; set; } = [];
    
    public string PlayerName  => Characters.FirstOrDefault(c => c.Type == CharacterType.Player)?.Name ?? "";

}