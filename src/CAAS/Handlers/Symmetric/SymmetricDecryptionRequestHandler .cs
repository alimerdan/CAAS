using CAAS.CryptoLib.Algorithms.Symmetric;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models.Symmetric;
using CAAS.Models.Symmetric.Decryption;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Symmetric
{
    public static class DecryptionRequestHandler
    {
        public static SymmetricDecryptionResponse Handle(SymmetricDecryptionRequest _decRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SymmetricDecryptionResponse res = ProcessRequest(_decRequest);

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

        private static SymmetricDecryptionResponse ProcessRequest(SymmetricDecryptionRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            byte[] data = Utils.TransformData(req.InputDataFormat, req.CipherData);
            byte[] key = Utils.TransformData(req.InputDataFormat, req.Key);
            ISymmetric processor = GetProcessor(algorithm);
            byte[] cipherData = processor.Decrypt(data, key);
            return new SymmetricDecryptionResponse()
            {
                Data = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }
    }
}
