using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.WiFi;

namespace NetworkScanner
{
    class ProbeAdapter:IProbeAdapter
    {
        bool ActiveDevice=false;
        WiFiAdapter WifiDevice;

        public ProbeAdapter()
        {
            SetWifiDevice();
        }

        public IEnumerable<IProbeResult> InitialScanResults()
        {
            List<IProbeResult> results = new List<IProbeResult>();
            if (ActiveDevice)
            {
                Scan();
                foreach (WiFiAvailableNetwork network in WifiDevice.NetworkReport.AvailableNetworks)
                {
                    results.Add(new ProbeResult(network.Bssid, network.Ssid, network.NetworkRssiInDecibelMilliwatts, network.ChannelCenterFrequencyInKilohertz));
                }
            }
            return results;
        }

        public IProbeRapport Probe(string ssid)
        {
            IProbeRapport result = new ProbeRapport();
            for (int i = 0; i < 10; i++)
            {
                Scan();
                WiFiAvailableNetwork targetNetwork = WifiDevice.NetworkReport.AvailableNetworks.Where(x => x.Ssid.ToLower() == ssid.ToLower()).FirstOrDefault();
                result.AddProbeResult(new ProbeResult(targetNetwork.Bssid, targetNetwork.Ssid, targetNetwork.NetworkRssiInDecibelMilliwatts, targetNetwork.ChannelCenterFrequencyInKilohertz));
            }
            return result;
        }

        private async void Scan()
        {
            if (ActiveDevice) await WifiDevice.ScanAsync();
            else return;
        }

        private async void SetWifiDevice()
        {
            var access = await WiFiAdapter.RequestAccessAsync();
            if (access != WiFiAccessStatus.Allowed)
            {
                throw new Exception("WiFiAccessStatus not allowed");
            }
            else
            {
                var WifiDeviceScan = await WiFiAdapter.FindAllAdaptersAsync();
                if (WifiDeviceScan.Count >= 1)
                {
                    WifiDevice = WifiDeviceScan[0];
                    ActiveDevice = true;
                }
            }
        }
    }
}
