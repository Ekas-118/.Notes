using DotNotes.Bus.Services;
using DotNotes.Bus.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using Windows.Storage;

namespace DotNotes;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton<IFileService>(x =>
            ActivatorUtilities.CreateInstance<WindowsFileService>(x,
                ApplicationData.Current.LocalFolder)
        );

        // ViewModels
        services.AddTransient<AllNotesViewModel>();
        services.AddTransient<NoteViewModel>();

        return services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }

    public IServiceProvider Services { get; }

    private Window? _window;

    public new static App Current => (App)Application.Current;
}