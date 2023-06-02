using CAAS.CryptoLib.Algorithms.Rng;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
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

            res.ProcessingTimeInMs = stopwatch.Elapsed.TotalMilliseconds;
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
            ValidateRequestDataFormats(req);
            byte[] digestedData = processor.GeneratePrng(req.Size);
            return new RngResponse()
            {
                Rng = Utils.TransformData(req.OutputDataFormat, digestedData)
            };
        }
        private static void ValidateRequestDataFormats(RngRequest req)
        {
            if (!ValidateOutputDataFormat(req.OutputDataFormat))
            {
                throw new NotSupportedDataFormatForOperationException(req.OutputDataFormat, "Rng Generate");
            }
        }

        private static bool ValidateOutputDataFormat(string dataFormat)
        {
            bool isSupportedOutputDataFormat = DataFormatValues.GetDataFormat(dataFormat) switch
            {
                DataFormat.ascii => false,
                DataFormat.utf8 => false,
                _ => true
            };
            return isSupportedOutputDataFormat;
        }
    }
}
