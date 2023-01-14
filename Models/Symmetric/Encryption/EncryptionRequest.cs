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
        public string Data { get; set; }
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
