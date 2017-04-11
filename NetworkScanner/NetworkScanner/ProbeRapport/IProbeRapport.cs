using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    interface IProbeRapport
    {
        double GetRSSIMean();
        int GetProbesSent();
        int GetFrequency();
        double calculateDistance(double levelInDb, double freqInMHz);
        void AddProbeResult(ProbeResult result);
        string String();
    }
}
