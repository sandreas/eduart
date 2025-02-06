using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using eduart.Storage;

namespace eduart.Browser.Storage;

public class LocalStorage : IStorage
{
    public async ValueTask<string?> GetItemAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            return await Task.FromResult(LocalStorageInterop.GetItem(key));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null;
        }
    }

    public async ValueTask SetItemAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        try
        {
            LocalStorageInterop.SetItem(key, value);
            await ValueTask.CompletedTask;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e); 
        }
    }

    public void Alert(string message)
    {
        try
        {
            LocalStorageInterop.Alert(message);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }
}