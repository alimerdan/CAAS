using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Symmetric.Decryption
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
        public string CipherData { get; set; }
        /// <summary>
        /// Encryption Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue("aes_cbc")]
        public string Algorithm { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("00112233445566770011223344556677")]
        public string Key { get; set; }

        /// <summary>
        /// Provided data format (hex, base64, ascii or utf8)
        /// </summary>
        [Required]
        [DefaultValue("hex")]
        public string InputDataFormat { get; set; }

        /// <summary>
        /// Required output data format (hex, base64, ascii or utf8)
        /// </summary>
        [Required]
        [DefaultValue("hex")]
        public string OutputDataFormat { get; set; }
    }
}
