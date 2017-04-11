using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    interface IProbeResult
    {
        string GetSSID();
        string GetMAC();
        double GetRSSI();
        int GetFrequency();
    }
}
