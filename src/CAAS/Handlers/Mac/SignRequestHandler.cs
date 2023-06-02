using CAAS.CryptoLib.Algorithms.Mac;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Models.Mac;
using CAAS.Models.Mac.Sign;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Mac
{
    public static class SignRequestHandler
    {
        public static SignResponse Handle(SignRequest _signRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SignResponse res = ProcessRequest(_signRequest);

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

        private static SignResponse ProcessRequest(SignRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            IMac processor = GetProcessor(algorithm);
            ValidateRequestDataFormats(req);
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            byte[] cipherData = processor.Generate(data, key);
            return new SignResponse()
            {
                Mac = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }
        private static void ValidateRequestDataFormats(SignRequest req)
        {
            if (!ValidateOutputDataFormat(req.OutputDataFormat))
            {
                throw new NotSupportedDataFormatForOperationException(req.OutputDataFormat, "Mac Sign");
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
