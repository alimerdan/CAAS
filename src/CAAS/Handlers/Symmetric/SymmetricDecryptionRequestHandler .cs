using CAAS.CryptoLib.Algorithms.Symmetric;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Models.Symmetric;
using CAAS.Models.Symmetric.Decryption;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Symmetric
{
    public static class SymmetricDecryptionRequestHandler
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

        private static SymmetricDecryptionResponse ProcessRequest(SymmetricDecryptionRequest req, string keyDataFormat = "hex")
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            ISymmetric processor = GetProcessor(algorithm);
            ValidateRequestDataFormats(req);
            byte[] data = Utils.TransformData(req.InputDataFormat, req.CipherData);
            byte[] key = Utils.TransformData(keyDataFormat, req.Key);
            byte[] cipherData = processor.Decrypt(data, key);
            return new SymmetricDecryptionResponse()
            {
                Data = Utils.TransformData(req.OutputDataFormat, cipherData)
            };
        }

        private static void ValidateRequestDataFormats(SymmetricDecryptionRequest req)
        {
            if (!ValidateInputDataFormat(req.InputDataFormat))
            {
                throw new NotSupportedDataFormatForOperationException(req.InputDataFormat, "Symmetric Decryption");
            }
        }

        private static bool ValidateInputDataFormat(string dataFormat)
        {
            bool isSupportedInputDataFormat = DataFormatValues.GetDataFormat(dataFormat) switch
            {
                DataFormat.ascii => false,
                DataFormat.utf8 => false,
                _ => true
            };
            return isSupportedInputDataFormat;
        }
    }
}
