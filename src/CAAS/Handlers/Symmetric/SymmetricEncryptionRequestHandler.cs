using CAAS.CryptoLib.Algorithms.Symmetric;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Symmetric;
using CAAS.Models.Symmetric.Encryption;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Symmetric
{
    public static class SymmetricEncryptionRequestHandler
    {
        public static SymmetricEncryptionResponse Handle(SymmetricEncryptionRequest _encRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SymmetricEncryptionResponse res = ProcessRequest(_encRequest);

            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.Elapsed.TotalMilliseconds;
            return res;
        }

        private static ISymmetric GetProcessor(string algorithm)
        {
            return SymmetricSupportedAlgorithmsValues.GetAlgorithm(algorithm) switch
            {
                SymmetricSupportedAlgorithms.aes_ecb_pkcs7 => new AesEcb(),
                SymmetricSupportedAlgorithms.aes_cbc_pkcs7 => new AesCbc(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
        }

        private static SymmetricEncryptionResponse ProcessRequest(SymmetricEncryptionRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            ISymmetric processor = GetProcessor(algorithm);
            byte[] cipherData = processor.Encrypt(data, key);
            return new SymmetricEncryptionResponse()
            {
                CipherData = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }
    }
}
