using Avalonia;
using Avalonia.Controls;

namespace TaskTracker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.AttachDevTools();
    }
}