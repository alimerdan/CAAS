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

            byte[] key = Utils.HexStringToByteArray(_encRequest.HexKey.Replace(" ", ""));
            byte[] data = Utils.HexStringToByteArray(_encRequest.HexData.Replace(" ", ""));
            EncryptionResponse res = new EncryptionResponse();
            ISymmetric processor = _encRequest.Algorithm switch
            {
                SymmetricSupportedAlgorithms.aes_ebc => new AesEcb(),
                SymmetricSupportedAlgorithms.aes_cbc => new AesCbc(),
                _ => throw new NotSupportedAlgorithmException(_encRequest.Algorithm.ToString().Trim().ToLower()),
            };
            res.HexCipherData = Utils.ByteArrayToHexString(processor.Encrypt(data, key));
            stopwatch.Stop();
            res.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds.ToString();
            return res;
        }
    }
}
