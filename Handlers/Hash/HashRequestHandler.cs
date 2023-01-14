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

            byte[] data = Utils.HexStringToByteArray(_hashRequest.HexData.Replace(" ", ""));
            string algorithm = _hashRequest.Algorithm.ToString().Trim().ToLower();
            HashResponse res = new HashResponse();
            IHash processor = _hashRequest.Algorithm switch
            {
                HashSupportedAlgorithms.sha256 => new Sha256(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
            res.HexHashData = Utils.ByteArrayToHexString(processor.Generate(data));
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
