namespace ViewModels
{
    #region Usings

    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using NativeWifi;
    using NetworkMonitors;
    using Profiler.Annotations;

    #endregion

    public class NetworkManagerViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Fields
        
        public readonly NetworkManagerObsolete NetworkManagerObsolete;

        #endregion

        #region Properties

        public ObservableCollection<string> ActiveNetworks
        {
            get
            {
                var networks = new ObservableCollection<string>(this.NetworkManagerObsolete.GetAvailableNetworkSSID());
                return networks;
            }
        }

        public WlanClient.WlanInterface ActiveWlanInterface
        {
            get { return this.NetworkManagerObsolete.ActiveInterface; }
        }

        #endregion

        #region Constructors

        public NetworkManagerViewModel()
        {
            this.NetworkManagerObsolete = new NetworkManagerObsolete();
        }

        #endregion

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}