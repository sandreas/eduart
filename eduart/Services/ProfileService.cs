using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.SimplePreferences;
using eduart.Models;

namespace eduart.Services;

public class ProfileService
{
    private const string StorageKey = "profiles";
    
    private readonly PreferencesService _preferences;
    private List<Profile> _profiles = [];
    public List<Profile> Profiles => _profiles;

    public ProfileService(PreferencesService preferences)
    {
        _preferences = preferences;
    }

    public Profile? Get(Guid id) => _profiles.FirstOrDefault(p => p.Id == id);
    public void Add(Profile profile) => _profiles.Add(profile);
    public void Update(Guid id, Profile profile) {
        Remove(id);
        Add(profile);
    }
    public void Remove(Guid id) => _profiles.RemoveAll(p => p.Id == id);

    public async Task<bool> LoadAsync()
    {
        var defaultValue = new List<Profile>();
        _profiles = await _preferences.GetAsync(StorageKey, defaultValue) ?? defaultValue;
        return true;
    }

    public async Task<bool> PersistAsync() => await _preferences.SetAsync(StorageKey, _profiles);
    
    
    
}