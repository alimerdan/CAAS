﻿using CAAS.CryptoLib.Algorithms.Hash;
using CAAS.CryptoLib.Interfaces;
using CAAS.Exceptions;
using CAAS.Models;
using CAAS.Models.Hash;
using CAAS.Utilities;
using System.Diagnostics;

namespace CAAS.Handlers.Hash
{
    public static class HashRequestHandler
    {
        public static HashResponse Handle(HashRequest _hashRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HashResponse res = ProcessRequest(_hashRequest);
            stopwatch.Stop();

            res.ProcessingTimeInMs = stopwatch.Elapsed.TotalMilliseconds;
            return res;
        }
        private static IHash GetProcessor(string algorithm)
        {
            return HashSupportedAlgorithmsValues.GetAlgorithm(algorithm) switch
            {
                HashSupportedAlgorithms.sha256 => new Sha256(),
                _ => throw new NotSupportedAlgorithmException(algorithm),
            };
        }

        private static HashResponse ProcessRequest(HashRequest req)
        {
            string algorithm = req.Algorithm.ToString().Trim().ToLower();
            IHash processor = GetProcessor(algorithm);
            ValidateRequestDataFormats(req);
            byte[] data = Utils.TransformData(req.InputDataFormat, req.Data);
            byte[] digestedData = processor.Generate(data);
            return new HashResponse()
            {
                Digest = Utils.TransformData(req.OutputDataFormat, digestedData)
            };
        }

        private static void ValidateRequestDataFormats(HashRequest req)
        {
            if (!ValidateOutputDataFormat(req.OutputDataFormat))
            {
                throw new NotSupportedDataFormatForOperationException(req.OutputDataFormat, "Digest");
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
