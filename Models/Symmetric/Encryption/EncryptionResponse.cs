using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Symmetric.Encryption
{
    /// <summary>
    /// Encryption Response Object
    /// </summary>
    public class EncryptionResponse
    {
        /// <summary>
        /// Encrypted/Ciphered Data in Hex Format
        /// </summary>
        [Required]
        [DefaultValue("C656C652E6656125139C219FD9F6EABB")]
        public string HexCipherData { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("11 ms")]
        public string ProcessingTimeInMs { get; set; }
    }
}
