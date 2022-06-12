using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    /// <summary>
    /// Decryption Response Object
    /// </summary>
    public class DecryptionResponse
    {
        /// <summary>
        /// Decrypted/Plain Data in Hex Format
        /// </summary>
        [Required]
        [DefaultValue("0011223344556677")]
        public string HexData { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("11 ms")]
        public string ProcessingTimeInMs { get; set; } 
    }
}
