using CAAS.Exceptions;
using System.Collections.Generic;
using System.ComponentModel;

namespace CAAS.Models.Hash
{
    public static class HashSupportedAlgorithmsValues
    {
        public readonly static HashSet<string> values = new HashSet<string>()
        {
            {"sha256" }
        };

        public static HashSupportedAlgorithms GetAlgorithm(string algorithmValue)
        {
            return algorithmValue.Trim().ToLower() switch
            {
                "sha256" => HashSupportedAlgorithms.sha256,
                _ => throw new NotSupportedAlgorithmException(algorithmValue),
            };
        }

    }
    public enum HashSupportedAlgorithms
    {
        sha256
    }
}
