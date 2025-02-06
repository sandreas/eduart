using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace eduart.Storage;

public class DictionaryStorage : IStorage
{
    private Dictionary<string, string> _values = new();
    public async ValueTask<string?> GetItemAsync(string key, CancellationToken cancellationToken = default)
    {
        if (_values.TryGetValue(key, out var value))
        {
            return await Task.FromResult(value);
        }

        return null;
    }

    public async ValueTask SetItemAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        _values[key] = value;
        await ValueTask.CompletedTask;
    }

    public void Alert(string message)
    {
        // not implementedŝ
    }
}