using CAAS.Exceptions;
using System.Collections.Generic;

namespace CAAS.Models
{
    public static class DataFormatValues
    {
        public readonly static HashSet<string> values = new HashSet<string>()
        {
            {"hex" },{"base64" },{"ascii" },{"utf8"}
        };

        public static DataFormat GetDataFormat(string dataFormatValue)
        {
            return dataFormatValue.Trim().ToLower() switch
            {
                "hex" => DataFormat.hex,
                "utf8" => DataFormat.utf8,
                "ascii" => DataFormat.ascii,
                "base64" => DataFormat.base64,
                _ => throw new NotSupportedDataFormatException(dataFormatValue),
            };
        }

    }

    public enum DataFormat
    {
        hex,
        base64,
        ascii,
        utf8
    }
}
