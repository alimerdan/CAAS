using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class DecryptionRequest
    {
        [Required]
        public string HexCipherData { get; set; }
        [Required]
        public string Algorithm { get; set; }
        [Required]
        public string HexKey { get; set; }
    }
}
