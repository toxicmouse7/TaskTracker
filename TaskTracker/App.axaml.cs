using System;
using System.Security.Authentication.ExtendedProtection;
using Application;
using Application.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Views;

namespace TaskTracker;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        var services = new ServiceCollection();

        services.AddApplication()
            .AddInfrastructure();
        
        Resources[typeof(IServiceProvider)] = services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = this.CreateInstance<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}