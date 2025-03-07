using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Reflection;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.SimplePreferences;
using Avalonia.SimpleRouter;
using eduart.Services;
using eduart.Storage;
using eduart.ViewModels;
using eduart.ViewModels.Game;
using eduart.Views;
using Microsoft.Extensions.DependencyInjection;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Projektanker.Icons.Avalonia.MaterialDesign;
using SoundFlow.Abstracts;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Enums;

namespace eduart;

public enum ProjectType
{
    Desktop,
    Browser
}
public partial class App : Application
{
    public static ProjectType ProjectType { get; set; } = ProjectType.Desktop;

    public static IStorage Storage { get; set; } = new DictionaryStorage();
    public override void Initialize()
    {
        IconProvider.Current
            .Register<FontAwesomeIconProvider>();

        AvaloniaXamlLoader.Load(this);
    }
    
    /*
    public override void OnFrameworkInitializationCompleted()
    {
            
        IServiceProvider services = ConfigureServices();
        
        _mediaPlayerService = services.GetRequiredService<MediaPlayerService>();

        
        
        Core.Initialize();

        var mainViewModel = services.GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel,
            };

            desktop.MainWindow.Closing += OnClosing;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
            singleViewPlatform.MainView.Unloaded += OnUnloaded;
        }

        base.OnFrameworkInitializationCompleted();
    }
*/
    public override void OnFrameworkInitializationCompleted()
    {

        IServiceProvider services = ConfigureServices();
        var mainViewModel = services.GetRequiredService<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
    
    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        // services.AddSingleton<IJSRuntime, JSRuntime>();

        // if (ProjectType == ProjectType.Browser)
        // {
            /*
            // services.AddScoped<IJSRuntime>(DefaultWebAssemblyJSRuntime.Instance);
            services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            // services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());

            services.AddSingleton<IStorage, BrowserStorageProvider>();
            */
            // services.AddSingleton<IStorage, DictionaryStorage>();
        // }
        // else
        // {
            // services.AddSingleton<IStorage>(s => Storage);
        // }

        // services.AddSingleton<PreferencesService>(s => new PreferencesService(new DebugBrowserStorage()));
        
        services.AddSingleton<PreferencesService>();
        services.AddSingleton<AssetsService>(s => new AssetsService("eduart", (Uri a, Uri? b) =>
        {
            return AssetLoader.Open(a, b);
        }));
        services.AddSingleton<ProfileService>();
        
        services.AddSingleton<GameService>();

        services.AddSingleton<AudioEngine>(s => new MiniAudioEngine(44100, Capability.Playback));
        services.AddSingleton<Mixer>(s => Mixer.Master);
        services.AddSingleton<AudioService>();

        services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)s.GetRequiredService(t)));

        services.AddSingleton<MainViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<EditProfileViewModel>();
        services.AddTransient<GameMainViewModel>();
        
        

        return services.BuildServiceProvider();
    }
}