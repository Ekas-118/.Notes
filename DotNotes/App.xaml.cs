using DotNotes.Bus.Services;
using DotNotes.Bus.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace DotNotes;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton<IFileService>(x =>
            ActivatorUtilities.CreateInstance<WindowsFileService>(x,
                            Windows.Storage.ApplicationData.Current.LocalFolder)
        );

        // ViewModels
        services.AddTransient<AllNotesViewModel>();
        services.AddTransient<NoteViewModel>();

        return services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

    public IServiceProvider Services { get; }

    private Window? m_window;

    public new static App Current => (App)Application.Current;
}