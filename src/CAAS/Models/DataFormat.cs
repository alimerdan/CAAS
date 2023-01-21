using System.Collections.Generic;

namespace CAAS.Models
{
    public static class DataFormatValues
    {
        public static HashSet<string> Values { get; } = new HashSet<string>()
        {
            {"hex" },{"base64" },{"ascii" },{"utf8"}
        };

        public static DataFormat GetDataFormat(string dataFormatValue)
        {
            DataFormat dataFormat = dataFormatValue.Trim().ToLower() switch
            {
                "hex" => DataFormat.hex,
                "utf8" => DataFormat.utf8,
                "ascii" => DataFormat.ascii,
                "base64" => DataFormat.base64,
                _ => DataFormat.notSupported,
            };
            return dataFormat;
        }

    }

    public enum DataFormat
    {
        hex,
        base64,
        ascii,
        utf8,
        notSupported
    }
}
