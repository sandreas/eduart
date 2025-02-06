using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using eduart;
using eduart.Browser.Storage;

internal sealed partial class Program
{
    private static async Task Main(string[] args)
    {
        // await JSHost.ImportAsync("PromisesShim", "/PromisesShim.js");

        await BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        App.Storage = new LocalStorage();
        // await JSHost.ImportAsync("PromisesShim", "/PromisesShim.js");
        return AppBuilder.Configure<App>();
    }
         
}