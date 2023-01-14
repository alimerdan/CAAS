using CAAS.CryptoLib.Algorithms.Hash;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
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
            string algorithm = _hashRequest.Algorithm.ToString().Trim().ToLower();
            HashResponse res = new HashResponse();
            byte[] data = Utils.TransformData(_hashRequest.InputDataFormat, _hashRequest.Data);
            IHash processor = HashSupportedAlgorithmsValues.GetAlgorithm(_hashRequest.Algorithm) switch
            {
                HashSupportedAlgorithms.sha256 => new Sha256(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
            byte[] digestedData = processor.Generate(data);
            res.Digest = Utils.TransformData(_hashRequest.OutputDataFormat, digestedData);
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
