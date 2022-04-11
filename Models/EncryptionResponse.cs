using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class EncryptionResponse
    {
        public string HexCipherData { get; set; }
        public string ProcessingTimeInMs { get; set; }
    }
}
