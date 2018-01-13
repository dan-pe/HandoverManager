namespace ViewModels
{
    #region Usings

    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;
    using System.Collections.Generic;
    using Profiler;

    #endregion

    public class UserMenuViewModel : INotifyPropertyChanged
    {
        #region Properties

        public List<UserProfile> UserProfiles{ get; set; }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Implementations of INotifyPropertyChanged

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
