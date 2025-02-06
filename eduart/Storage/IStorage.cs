using System.Threading;
using System.Threading.Tasks;

namespace eduart.Storage;

public interface IStorage
{
    public ValueTask<string?> GetItemAsync(string key, CancellationToken cancellationToken = default);
    public ValueTask SetItemAsync(string key, string value, CancellationToken cancellationToken = default);

    public void Alert(string message);

}