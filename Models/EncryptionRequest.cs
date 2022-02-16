using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class EncryptionRequest
    {
        [Required]
        public string HexData { get; set; }
        [Required]
        public string Algorithm { get; set; }
        [Required]
        public string HexKey { get; set; }
    }
}
