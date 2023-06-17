using CAAS.CryptoLib.Algorithms.Mac;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Models.Mac;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Mac
{
    public static class MacRequestHandler
    {
        public static MacResponse Handle(MacRequest _macRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            MacResponse res = ProcessRequest(_macRequest);

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

        private static MacResponse ProcessRequest(MacRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            IMac processor = GetProcessor(algorithm);
            ValidateRequestDataFormats(req);
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            byte[] cipherData = processor.Generate(data, key);
            return new MacResponse()
            {
                Mac = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }
        private static void ValidateRequestDataFormats(MacRequest req)
        {
            if (!ValidateOutputDataFormat(req.OutputDataFormat))
            {
                throw new NotSupportedDataFormatForOperationException(req.OutputDataFormat, "Mac Generation");
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
