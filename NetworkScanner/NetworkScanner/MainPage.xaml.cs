using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NetworkScanner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<string> NetworkLables = new List<string>();
        IEnumerable<IProbeResult> DiscoveredNetworks;
        ProbeAdapter Prober = new ProbeAdapter();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SetNetworkLables()
        {
            ScanResultsComboBox.SelectedIndex = -1;   
            NetworkLables = new List<string>();
            for (int i = 0; i < DiscoveredNetworks.Count(); i++)
            {
                NetworkLables.Add(String.Format("{0}. MAC: {1}, SSID: {2}, RSSI: {3}",i, DiscoveredNetworks.ElementAt(i).GetMAC(), DiscoveredNetworks.ElementAt(i).GetSSID(), DiscoveredNetworks.ElementAt(i).GetRSSI()));
            }
            if (NetworkLables.Count == 0)
            {
                NetworkLables.Add("No network found");
            }
            ScanResultsComboBox.ItemsSource = NetworkLables;
        }

        private void InitialScanButton_Click(object sender, RoutedEventArgs e)
        {
            DiscoveredNetworks = Prober.InitialScanResults();
            SetNetworkLables();
        }

        private void ScanResultsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ScanResultsComboBox.SelectedIndex;
            if (index > 0)
            {
                ChosenMACTxtBox.Text = DiscoveredNetworks.ElementAt(index).GetMAC();
                ChosenSSIDTxtBox.Text = DiscoveredNetworks.ElementAt(index).GetSSID();
                double frequency = DiscoveredNetworks.ElementAt(index).GetFrequency()/1000;
                CurrentFrequencyTxtBox.Text = frequency.ToString();
            }
            else
            {
                ChosenMACTxtBox.Text = "";
                ChosenSSIDTxtBox.Text = "";
                CurrentFrequencyTxtBox.Text = "";
            }
        }

        private void DistanceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem x = (ComboBoxItem)DistanceComboBox.SelectionBoxItem;
        }

        private void ProbeButton_Click(object sender, RoutedEventArgs e)
        {
            IProbeRapport rapport = Prober.Probe(ChosenSSIDTxtBox.Text);
            RSSIMeanCurrentTxtBox.Text = rapport.GetRSSIMean().ToString();
            CurrentAPDistanceTxtBox.Text = rapport.calculateDistance(rapport.GetRSSIMean(), rapport.GetFrequency() / 1000).ToString();
        }
    }
}
