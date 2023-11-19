using Avalonia;
using Avalonia.Controls;

namespace TaskTracker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void Minimize()
    {
        WindowState = WindowState.Minimized;
    }
}