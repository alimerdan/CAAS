using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Mac.Sign
{
    /// <summary>
    /// Signing Request Object
    /// </summary>
    public class SignRequest
    {
        /// <summary>
        /// Plain Data Value
        /// </summary>
        [Required]
        [DefaultValue("0011223344556677")]
        public string Data { get; set; }
        /// <summary>
        /// Signing Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue("aes128_cmac")]
        public string Algorithm { get; set; }
        /// <summary>
        /// Key Value
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
