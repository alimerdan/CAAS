using CAAS.CryptoLib.Algorithms.Mac;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Mac;
using CAAS.Models.Mac.Verify;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Mac
{
    public static class VerifyRequestHandler
    {
        public static VerifyResponse Handle(VerifyRequest _verifyRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            VerifyResponse res = ProcessRequest(_verifyRequest);

            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.Elapsed.TotalMilliseconds;
            return res;
        }

        private static IMac GetProcessor(string algorithm)
        {
            return MacSupportedAlgorithmsValues.GetAlgorithm(algorithm) switch
            {
                MacSupportedAlgorithms.aes128_cmac => new Aes128Cmac(),
                MacSupportedAlgorithms.sha256_hmac => new Sha256Hmac(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
        }

        private static VerifyResponse ProcessRequest(VerifyRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            byte[] signature = Utils.TransformData(req.InputDataFormat, req.Signature);
            IMac processor = GetProcessor(algorithm);
            bool isVerified = processor.Verify(data, key, signature);
            return new VerifyResponse()
            {
                IsVerified = isVerified
            };
        }
    }
}
