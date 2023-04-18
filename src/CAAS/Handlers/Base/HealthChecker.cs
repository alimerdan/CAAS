using CAAS.Models.Base;
using System.Diagnostics;

namespace CAAS.Handlers.Base
{
    public static class HealthChecker
    {
        public static HealthCheckResponse CheckHealth()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HealthCheckResponse res = ProcessRequest();

            stopwatch.Stop();

            res.ProcessingTimeInMs = stopwatch.Elapsed.TotalMilliseconds;
            return res;
        }
        private static HealthCheckResponse ProcessRequest()
        {
            return new HealthCheckResponse()
            {
                Status = "Iam Healthy"
            };
        }
    }
}
