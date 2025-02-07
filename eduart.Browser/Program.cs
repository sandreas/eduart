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

        
        // moduleName will be used in [JSImport("<functionName>", "<ModuleName>")]
        //   [JSImport("<functionName>", "LocalStorage")]
        // moduleUrl will be referenced from wwwroot and needs a leading /
        await JSHost.ImportAsync("LocalStorage", 
            "/localstorage.js");
        
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