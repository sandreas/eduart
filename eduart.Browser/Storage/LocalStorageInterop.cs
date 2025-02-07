using System.Runtime.InteropServices.JavaScript;

namespace eduart.Browser.Storage;

public static partial class LocalStorageInterop
{
    public static void SetItem(string key, string value)
    {
        _setLocalStorage(key, value);
    }
    
    public static string GetItem(string key)
    {
        return _getLocalStorage(key);
    }

    public static void Remove(string key)
    {
        _removeLocalStorage(key);
    }

    public static void Clear()
    {
        _clearLocalStorage();
    }
    
    public static void Alert(string key)
    {
        _alertTest(key);
    }
    
    [JSImport("setLocalStorage", "LocalStorage")]
    internal static partial void _setLocalStorage(string key, string value);

    [JSImport("getLocalStorage", "LocalStorage")]
    internal static partial string _getLocalStorage(string key);
    
    [JSImport("removeLocalStorage", "LocalStorage")]
    internal static partial void _removeLocalStorage(string key);
    
    [JSImport("clearLocalStorage", "LocalStorage")]
    internal static partial void _clearLocalStorage();
    
    [JSImport("alertTest", "LocalStorage")]
    internal static partial void _alertTest(string message);
}
/*
public static partial class Interop
{
    [JsonSerializable(typeof(List<Item>))]
    private partial class ItemListSerializerContext : JsonSerializerContext { }
            
    public static List<Item> getLocalStorage()
    {
        var json = _getLocalStorage();
        return JsonSerializer.Deserialize(json, ItemListSerializerContext.Default.ListItem) ?? new List<Item>();
    }

    public static void setLocalStorage(List<Item> items)
    {
        var json = JsonSerializer.Serialize(items, ItemListSerializerContext.Default.ListItem);
        _setLocalStorage(json);
    }

    [JSImport("setLocalStorage", "todoMVC/store.js")]
    internal static partial void _setLocalStorage(string json);

    [JSImport("getLocalStorage", "todoMVC/store.js")]
    internal static partial string _getLocalStorage();
}
*/