using System.Collections.Generic;

namespace CAAS.Models.Hash
{
    public static class HashSupportedAlgorithmsValues
    {
        public static HashSet<string> Values { get; } = new HashSet<string>()
        {
            {"sha256" }
        };

        public static HashSupportedAlgorithms GetAlgorithm(string algorithmValue)
        {
            return algorithmValue.Trim().ToLower() switch
            {
                "sha256" => HashSupportedAlgorithms.sha256,
                _ => HashSupportedAlgorithms.notSupported,
            };
        }

    }
    public enum HashSupportedAlgorithms
    {
        sha256,
        notSupported
    }
}
