using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class EncryptionResponse
    {
        [Required]
        public string HexCipherData { get; set; }
        [Required]
        public string ProcessingTimeInMs { get; set; }
    }
}
