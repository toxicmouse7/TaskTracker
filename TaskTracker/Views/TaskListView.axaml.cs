using Application.ViewModels;
using Avalonia.Controls;

namespace TaskTracker.Views;

public partial class TaskListView : UserControl
{
    public TaskListView()
    {
        InitializeComponent();
    }

    public async void ExportToClipboard()
    {
        if (DataContext is not TaskListViewModel dataContext)
            return;

        var exportString = await dataContext.GetExportString();

        var topLevel = TopLevel.GetTopLevel(this);
        await topLevel?.Clipboard?.SetTextAsync(exportString)!;
    }
}