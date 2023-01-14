using CAAS.Exceptions;
using System.Collections.Generic;

namespace CAAS.Models.Symmetric
{

    public static class SymmetricSupportedAlgorithmsValues
    {
        public readonly static HashSet<string> values = new HashSet<string>()
        {
            {"aes_cbc" },{"aes_ebc"}
        };

        public static SymmetricSupportedAlgorithms GetAlgorithm(string algorithmValue)
        {
            return algorithmValue.Trim().ToLower() switch
            {
                "aes_cbc" => SymmetricSupportedAlgorithms.aes_cbc,
                "aes_ebc" => SymmetricSupportedAlgorithms.aes_ebc,
                _ => throw new NotSupportedAlgorithmException(algorithmValue),
            };
        }

    }

    public enum SymmetricSupportedAlgorithms
    {
        aes_cbc,
        aes_ebc
    }
}
