using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    /// <summary>
    /// Decryption Request Object
    /// </summary>
    public class DecryptionRequest
    {
        /// <summary>
        /// Encrypted/Ciphered Data in Hex Format
        /// </summary>
        /// <example>123</example>
        [Required]
        [DefaultValue("C656C652E6656125139C219FD9F6EABB")]
        public string HexCipherData { get; set; }
        /// <summary>
        /// Encryption Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue(SupportedAlgorithms.aes_cbc)]
        public SupportedAlgorithms Algorithm { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("00112233445566770011223344556677")]
        public string HexKey { get; set; }
    }
}
