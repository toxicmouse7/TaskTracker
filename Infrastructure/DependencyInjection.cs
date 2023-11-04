using Domain.Abstractions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSqlite<ApplicationDbContext>(
            "Data Source=task-tracker.db");
        
        serviceCollection.AddTransient<IApplicationDbContext>(
            services => services.GetRequiredService<ApplicationDbContext>());
        
        serviceCollection.AddSingleton<ITaskRepository, TaskRepository>();

        return serviceCollection;
    }
}