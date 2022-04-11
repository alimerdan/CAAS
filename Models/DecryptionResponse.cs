using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    public class DecryptionResponse
    {
        public string HexData { get; set; }
        public string ProcessingTimeInMs { get; set; } 
    }
}
