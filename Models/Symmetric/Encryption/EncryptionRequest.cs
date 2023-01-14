using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Symmetric.Encryption
{
    /// <summary>
    /// Encryption Request Object
    /// </summary>
    public class EncryptionRequest
    {
        /// <summary>
        /// Plain Data in Hex Format
        /// </summary>
        [Required]
        [DefaultValue("0011223344556677")]
        public string HexData { get; set; }
        /// <summary>
        /// Encryption Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue(SymmetricSupportedAlgorithms.aes_cbc)]
        public SymmetricSupportedAlgorithms Algorithm { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("00112233445566770011223344556677")]
        public string HexKey { get; set; }
    }
}
