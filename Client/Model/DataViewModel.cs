using System.Collections.ObjectModel;
using AcraBackend.Common.Database.Dao;
using AcraBackend.Common.Database.Model;

namespace AcraBackend.Client.Model
{
    public class DataViewModel : ObservableObject
    {
        private readonly ReportsDao _dao = new();

        public DataViewModel()
        {
            PackageNames = new ObservableCollection<string>(_dao.GetPackageNames());
        }

        private ObservableCollection<string> _packageNames;

        public ObservableCollection<string> PackageNames
        {
            get => _packageNames;
            set
            {
                _packageNames = value;
                RaisePropertyChangedEvent(nameof(PackageNames));

                if (PackageNames.Count > 0)
                    SelectedPackageName = PackageNames[0];
            }
        }

        private string _selectedPackageName;

        public string SelectedPackageName
        {
            get => _selectedPackageName;
            set
            {
                _selectedPackageName = value;
                RaisePropertyChangedEvent(nameof(SelectedPackageName));

                Reports = new ObservableCollection<FatalReport>(_dao.GetFatalReports(value));

                if (Reports.Count > 0)
                    SelectedReport = Reports[0];
            }
        }

        private ObservableCollection<FatalReport> _reports;

        public ObservableCollection<FatalReport> Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                RaisePropertyChangedEvent(nameof(Reports));
            }
        }

        private FatalReport _selectedReport;

        public FatalReport SelectedReport
        {
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                RaisePropertyChangedEvent(nameof(SelectedReport));
            }
        }

        public void CloseReport()
        {
            _dao.CloseReport(SelectedReport);
        }

        public void DeleteReport()
        {
            _dao.DeleteReport(SelectedReport);
        }

        public void RefreshData()
        {
            SelectedPackageName = _selectedPackageName;
        }
    }
}