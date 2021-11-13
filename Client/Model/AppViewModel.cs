using System.ComponentModel;

namespace AcraBackend.Client.Model
{
    public class AppViewModel : ObservableObject
    {
        public DataViewModel DataViewModel { get; } = new();
    }

    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
