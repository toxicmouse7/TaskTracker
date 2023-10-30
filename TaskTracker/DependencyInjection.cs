using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTracker;

public static class DependencyInjection
{
    public static IServiceProvider GetServiceProvider(this IResourceHost control)
    {
        var test = control.FindResource(typeof(IServiceProvider))!;
        return (IServiceProvider)test;
    }

    public static T CreateInstance<T>(this IResourceHost control)
    {
        return (T)control.CreateInstance(typeof(T));
    }
    
    public static object CreateInstance(this IResourceHost control, Type type)
    {
        return ActivatorUtilities.CreateInstance(control.GetServiceProvider(), type);
    }
}