using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetworkMonitors;
using Profiler.Annotations;

namespace ViewModels
{
    public class NetworkManagerViewModel : INotifyPropertyChanged
    {

        private readonly NetworkManager NetworkManager;

        public ObservableCollection<string> ActiveNetworks
        {
            get
            {
                var networks = new ObservableCollection<string>(this.NetworkManager.AvailableNetworkSSID);
                return networks;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NetworkManagerViewModel()
        {
            this.NetworkManager = new NetworkManager();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}