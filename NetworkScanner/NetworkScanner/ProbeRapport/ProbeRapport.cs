using System;
using System.Collections.Generic;

namespace NetworkScanner
{
    public class ProbeRapport:IProbeRapport
    {
        List<IProbeResult> Results;

        public ProbeRapport()
        {
            Results = new List<IProbeResult>();
        }

        public void AddProbeResult(ProbeResult result)
        {
            Results.Add(result);
        }

        public double calculateDistance(double levelInDb, double freqInMHz)
        {
            double exp = (27.55 - (20 * Math.Log10(freqInMHz)) + Math.Abs(levelInDb)) / 20.0;
            return Math.Pow(10.0, exp);
        }

        public int GetFrequency()
        {
            return Results[0].GetFrequency();
        }

        public int GetProbesSent()
        {
            return Results.Count;
        }

        public double GetRSSIMean()
        {
            double total = 0;
            foreach (IProbeResult result in Results)
            {
                total += result.GetRSSI();
            }
            return total / Results.Count;
        }

        public string String()
        {
            throw new NotImplementedException();
        }
    }
}