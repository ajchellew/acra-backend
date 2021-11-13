using System.Windows;
using System.Windows.Controls;
using AcraBackend.Client.Model;

namespace AcraBackend.Client
{
    /// <summary>
    /// Interaction logic for FatalReportControl.xaml
    /// </summary>
    public partial class FatalReportControl : UserControl
    {
        public FatalReportControl()
        {
            InitializeComponent();
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (DataViewModel)DataContext;
            viewModel.CloseReport();
            viewModel.RefreshData();
        }
        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (DataViewModel)DataContext;
            viewModel.DeleteReport();
            viewModel.RefreshData();
        }
    }
}
