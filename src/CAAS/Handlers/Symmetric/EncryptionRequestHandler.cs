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

            byte[] key = Utils.TransformData(_encRequest.InputDataFormat, _encRequest.Key);
            byte[] data = Utils.TransformData(_encRequest.InputDataFormat, _encRequest.Data);
            EncryptionResponse res = new EncryptionResponse();
            ISymmetric processor = SymmetricSupportedAlgorithmsValues.GetAlgorithm(_encRequest.Algorithm) switch
            {
                SymmetricSupportedAlgorithms.aes_ecb_pkcs7 => new AesEcb(),
                SymmetricSupportedAlgorithms.aes_cbc_pkcs7 => new AesCbc(),
                _ => throw new NotSupportedAlgorithmException(_encRequest.Algorithm.ToString().Trim().ToLower()),
            };
            byte[] cipherData = processor.Encrypt(data, key);
            res.CipherData = Utils.TransformData(_encRequest.OutputDataFormat, cipherData);
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
            return res;
        }
    }
}
