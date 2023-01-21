using System.Collections.Generic;

namespace CAAS.Models.Symmetric
{

    public static class SymmetricSupportedAlgorithmsValues
    {
        private static readonly HashSet<string> values = new HashSet<string>()
        {
            {"aes_cbc_pkcs7" },{"aes_ecb_pkcs7"}
        };

        public static HashSet<string> Values => values;

        public static SymmetricSupportedAlgorithms GetAlgorithm(string algorithmValue)
        {
            return algorithmValue.Trim().ToLower() switch
            {
                "aes_cbc_pkcs7" => SymmetricSupportedAlgorithms.aes_cbc_pkcs7,
                "aes_ecb_pkcs7" => SymmetricSupportedAlgorithms.aes_ecb_pkcs7,
                _ => SymmetricSupportedAlgorithms.notSupported,
            };
        }

    }

    public enum SymmetricSupportedAlgorithms
    {
        aes_cbc_pkcs7,
        aes_ecb_pkcs7,
        notSupported
    }
}
