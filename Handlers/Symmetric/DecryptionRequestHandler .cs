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

            byte[] key = Utils.HexStringToByteArray(_decRequest.HexKey.Replace(" ", ""));
            byte[] cipherData = Utils.HexStringToByteArray(_decRequest.HexCipherData.Replace(" ", ""));
            DecryptionResponse res = new DecryptionResponse();
            ISymmetric processor = _decRequest.Algorithm switch
            {
                SymmetricSupportedAlgorithms.aes_ebc => new AesEcb(),
                SymmetricSupportedAlgorithms.aes_cbc => new AesCbc(),
                _ => throw new NotSupportedAlgorithmException(_decRequest.Algorithm.ToString().Trim().ToLower()),
            };
            res.HexData = Utils.ByteArrayToHexString(processor.Decrypt(cipherData, key));
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
