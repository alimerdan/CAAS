using CAAS.CryptoLib.Algorithms.Hash;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Hash;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Hash
{
    public static class HashRequestHandler
    {
        public static HashResponse Handle(HashRequest _hashRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string digestedData = ProcessRequest(_hashRequest);
            stopwatch.Stop();

            return new HashResponse()
            {
                Digest = digestedData,
                ProcessingTimeInMs = stopwatch.ElapsedMilliseconds
            };
        }
        private static IHash GetProcessor(string algorithm)
        {
            return HashSupportedAlgorithmsValues.GetAlgorithm(algorithm) switch
            {
                HashSupportedAlgorithms.sha256 => new Sha256(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
        }
        private static byte[] FormatRequestData(string inputDataFormat, string data)
        {
            return Utils.TransformData(inputDataFormat, data);
        }
        private static string FormatResponseData(string outputDataFormat, byte[] data)
        {
            return Utils.TransformData(outputDataFormat, data);
        }
        private static string ProcessRequest(HashRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            byte[] data = FormatRequestData(req.InputDataFormat, req.Data);
            IHash processor = GetProcessor(algorithm);
            byte[] digestedData = processor.Generate(data);
            return FormatResponseData(req.OutputDataFormat, digestedData);
        }
    }
}
