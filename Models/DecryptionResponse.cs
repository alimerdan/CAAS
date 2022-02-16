using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class DecryptionResponse
    {
        [Required] 
        public string HexData { get; set; }
        [Required]
        public string ProcessingTimeInMs { get; set; } 
    }
}
