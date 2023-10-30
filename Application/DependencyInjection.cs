using Application.Services;
using Application.ViewModels;
using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<ITaskTrackingService, TaskTrackingService>();
        serviceProvider.AddTransient<MainWindowViewModel>();
        serviceProvider.AddTransient<TaskListViewModel>();

        return serviceProvider;
    }
}