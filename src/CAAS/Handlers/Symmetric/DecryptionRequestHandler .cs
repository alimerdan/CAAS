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
        public static DecryptionResponse Handle(DecryptionRequest _decRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string algorithm = _decRequest.Algorithm.ToString().Trim().ToLower();
            byte[] key = Utils.TransformData(_decRequest.InputDataFormat, _decRequest.Key);
            byte[] cipherData = Utils.TransformData(_decRequest.InputDataFormat, _decRequest.CipherData);
            DecryptionResponse res = new DecryptionResponse();
            ISymmetric processor = SymmetricSupportedAlgorithmsValues.GetAlgorithm(_decRequest.Algorithm) switch
            {
                SymmetricSupportedAlgorithms.aes_ecb_pkcs7 => new AesEcb(),
                SymmetricSupportedAlgorithms.aes_cbc_pkcs7 => new AesCbc(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
            byte[] plainData = processor.Decrypt(cipherData, key);
            res.Data = Utils.TransformData(_decRequest.OutputDataFormat, plainData);
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
            return res;
        }
    }
}
