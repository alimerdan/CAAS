using CAAS.CryptoLib.Algorithms.Rng;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Rng;
using CAAS.Utilities;
using System.Diagnostics;
namespace CAAS.Handlers.Rng
{
    public static class RngRequestHandler
    {
        public static RngResponse Handle(RngRequest _rngRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            RngResponse res = ProcessRequest(_rngRequest);
            stopwatch.Stop();

            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
            return res;
        }
        private static IRng GetProcessor(string algorithm)
        {
            return RngSupportedAlgorithmsValues.GetAlgorithm(algorithm) switch
            {
                RngSupportedAlgorithms.csprng => new Csprng(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
        }

        private static RngResponse ProcessRequest(RngRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            IRng processor = GetProcessor(algorithm);
            byte[] digestedData = processor.GeneratePrng(req.Size);
            return new RngResponse()
            {
                Rng = Utils.TransformData(req.OutputDataFormat, digestedData)
            };
        }
    }
}
