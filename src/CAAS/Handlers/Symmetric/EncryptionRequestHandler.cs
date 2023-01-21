using CAAS.CryptoLib.Algorithms.Symmetric;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Symmetric;
using CAAS.Models.Symmetric.Encryption;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Symmetric
{
    public static class EncryptionRequestHandler
    {
        public static EncryptionResponse Handle(EncryptionRequest _encRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            EncryptionResponse res = ProcessRequest(_encRequest);

            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
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

        private static EncryptionResponse ProcessRequest(EncryptionRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            ISymmetric processor = GetProcessor(algorithm);
            byte[] cipherData = processor.Encrypt(data, key);
            return new EncryptionResponse()
            {
                CipherData = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }
    }
}
