﻿using System.Collections.Generic;

namespace CAAS.Models.Rng
{
    public static class RngSupportedAlgorithmsValues
    {
        public static HashSet<string> Values { get; } = new HashSet<string>()
        {
            {"csprng" }
        };

        public static RngSupportedAlgorithms GetAlgorithm(string algorithmValue)
        {
            return algorithmValue.Trim().ToLower() switch
            {
                "csprng" => RngSupportedAlgorithms.csprng,
                _ => RngSupportedAlgorithms.notSupported,
            };
        }

    }
    public enum RngSupportedAlgorithms
    {
        csprng,
        notSupported
    }
}
