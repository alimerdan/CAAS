using System.Collections.Generic;

namespace CAAS.Models.Mac
{

    public static class MacSupportedAlgorithmsValues
    {
        public static readonly HashSet<string> values = new HashSet<string>()
        {
            {"aes128_cmac" },{"sha256_hmac"}
        };

        public static MacSupportedAlgorithms GetAlgorithm(string algorithmValue)
        {
            return algorithmValue.Trim().ToLower() switch
            {
                "aes128_cmac" => MacSupportedAlgorithms.aes128_cmac,
                "sha256_hmac" => MacSupportedAlgorithms.sha256_hmac,
                _ => MacSupportedAlgorithms.notSupported,
            };
        }

    }

    public enum MacSupportedAlgorithms
    {
        aes128_cmac,
        sha256_hmac,
        notSupported
    }
}
