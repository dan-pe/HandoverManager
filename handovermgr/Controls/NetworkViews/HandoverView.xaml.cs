#region Usings

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExcelLogger;
using HandoverAlgorithmBase.NovelAlgorithm;
using Logger;
using Microsoft.Win32;
using Profiler;
using RadioNetworks;

#endregion

namespace handovermgr.Controls.NetworkViews
{
    /// <summary>
    /// Interaction logic for HandoverView.xaml
    /// </summary>
    public sealed partial class HandoverView : Window
    {
        #region Private Fields

        /// <summary>
        /// Radio networks list.
        /// </summary>
        private readonly List<RadioNetworkModel> _radioNetworksList;

        private List<NovelNetworkModel> _novelNetworksList;

        /// <summary>
        /// Margins thickness.
        /// </summary>
        private int _marginThickness = 5;

        UserProfile selectedUserProfile;

        #endregion

        /// <summary>
        /// The Handover window.
        /// </summary>
        /// <param name="radioNetowrksList">
        /// List of networks retrieved from main window.
        /// </param>
        public HandoverView(ObservableCollection<RadioNetworkModel> radioNetowrksList, ComboBox novelProfileComboBox)
        {
            InitializeComponent();
            _radioNetworksList = new List<RadioNetworkModel>(radioNetowrksList);

            PrepareHandoverNetworksView(_radioNetworksList, novelProfileComboBox);
        }

        /// <summary>
        /// Populates handover view window with networks,
        /// based on the Novel Handover.
        /// </summary>
        /// <param name="radioNetworksList">
        /// Radio networks list.
        /// </param>
        /// <param name="novelProfileComboBox">
        /// Combo box indicating chosen profile.
        /// </param>
        private void PrepareHandoverNetworksView(List<RadioNetworkModel> radioNetworksList, ComboBox novelProfileComboBox)
        {
            Logger.Logger.AddMessage("Handover evaluation started.");

            this.selectedUserProfile = Profiler.ProfileManager.Instance.GetProfileByName(novelProfileComboBox.Text);

            NovelHandoverAlgorithm novelHandover = new NovelHandoverAlgorithm(_radioNetworksList,this.selectedUserProfile);

            novelHandover.RunSelection();
            var resultNetwork = novelHandover.SelectResultNetwork();
            this._novelNetworksList = novelHandover.NovelNetworkModels;

            foreach (var radioNetwork in novelHandover.NovelNetworkModels)
            {
                AddHandoverViewItem(radioNetwork, resultNetwork);
            }
        }

        /// <summary>
        /// Adds single handover item to view.
        /// </summary>
        /// <param name="radioNetwork"></param>
        /// <param name="resultNetwork"></param>
        private void AddHandoverViewItem(NovelNetworkModel radioNetwork, NovelNetworkModel resultNetwork)
        {
            var stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            var label = new Label {Content = radioNetwork.RadioNetworkModel.NetworkName};
            label.Margin = new Thickness(_marginThickness);
            label.Width = 90;

            var textBox = new TextBox
            {
                Text = radioNetwork.GrcFactor.ToString("N4")
            };
            textBox.Margin = new Thickness(_marginThickness);
            textBox.Width = 70;
            textBox.Height = 20;
            textBox.TextAlignment = TextAlignment.Center;

            if (radioNetwork.RadioNetworkModel.NetworkName.Equals(resultNetwork.RadioNetworkModel.NetworkName))
            {
                textBox.Background = new SolidColorBrush(Color.FromRgb(0, 204, 102));
                Logger.Logger.AddMessage(
                    $"Result network: {radioNetwork.RadioNetworkModel.NetworkName} Handover factor: {resultNetwork.GrcFactor:N4}",
                    MessageThreshold.SUCCESS);
            }

            stackpanel.Children.Add(label);
            stackpanel.Children.Add(textBox);

            HandoverPanel.Children.Add(stackpanel);
        }

        /// <summary>
        /// Generates Xls format log.
        /// </summary>
        private void GenerateXlsButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.ShowDialog();

            ExcelLog excelLog = new ExcelLog(saveFileDialog.FileName, this._novelNetworksList, this.selectedUserProfile);
        }
    }
}
