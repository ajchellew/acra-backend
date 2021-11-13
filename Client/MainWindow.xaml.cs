using System.Windows;
using AcraBackend.Client.Model;
using MahApps.Metro.Controls;

namespace AcraBackend.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly AppViewModel _viewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DataViewModel.PackageNames.Count == 0)
                MessageBox.Show(this, "No reports to show");
        }
    }
}
