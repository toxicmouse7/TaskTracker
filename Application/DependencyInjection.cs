using Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddTransient<MainWindowViewModel>();
        serviceProvider.AddTransient<TaskListViewModel>();

        serviceProvider.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return serviceProvider;
    }
}