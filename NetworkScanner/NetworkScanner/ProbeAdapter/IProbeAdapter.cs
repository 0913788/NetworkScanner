using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    interface IProbeAdapter
    {
        IProbeRapport Probe(string ssid);
        IEnumerable<IProbeResult> InitialScanResults();
    }
}
