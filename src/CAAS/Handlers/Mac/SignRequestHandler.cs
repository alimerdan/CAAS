using CAAS.CryptoLib.Algorithms.Mac;
using CAAS.CryptoLib.Algorithms.Symmetric;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Mac;
using CAAS.Models.Mac.Sign;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Mac
{
    public class SignRequestHandler
    {
        public static SignResponse Handle(SignRequest _signRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SignResponse res = ProcessRequest(_signRequest);

            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
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

        private static SignResponse ProcessRequest(SignRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            IMac processor = GetProcessor(algorithm);
            byte[] cipherData = processor.Generate(data, key);
            return new SignResponse()
            {
                Mac = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }
    }
}
