using CAAS.Models;
using System.Diagnostics;

namespace CAAS.Handlers
{
    public static class HealthChecker
    {
        public static HealthCheckResponse CheckHealth()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HealthCheckResponse res = new HealthCheckResponse(); ;
            res.Status = "Iam Healthy";
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
            return res;
        }
    }
}
