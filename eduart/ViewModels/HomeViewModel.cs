using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eduart.Storage;

namespace eduart.ViewModels;

public partial class HomeViewModel: ViewModelBase
{
    private readonly IStorage _storage;

    public HomeViewModel(IStorage storage)
    {
        _storage = storage;
    }
    [ObservableProperty]
    private int _counter;


    [RelayCommand]
    private async Task IncrementCounter()
    {
        ++Counter;
        _storage.Alert("test");
        // await _storage.SetItemAsync("counter",  Counter.ToString());
    }
    
    [RelayCommand]
    private async Task RestoreCounter()
    {
        var counter = await _storage.GetItemAsync("counter");
        if (int.TryParse(counter, out var counterValue))
        {
            Counter = counterValue;
        }
        else
        {
            Counter = -1;
        }
    }
}