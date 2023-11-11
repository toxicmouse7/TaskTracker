using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTracker;

public static class DependencyInjection
{
    private static IServiceProvider GetServiceProvider(this IResourceHost control)
    {
        return (IServiceProvider)control.FindResource(typeof(IServiceProvider))!;
    }

    public static T CreateInstance<T>(this IResourceHost control)
    {
        return (T)control.CreateInstance(typeof(T));
    }

    private static object CreateInstance(this IResourceHost control, Type type)
    {
        return ActivatorUtilities.CreateInstance(control.GetServiceProvider(), type);
    }
}