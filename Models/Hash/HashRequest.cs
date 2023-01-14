using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Hash
{
    /// <summary>
    /// Hash Request Object
    /// </summary>
    public class HashRequest
    {
        /// <summary>
        /// Plain Data
        /// </summary>
        [Required]
        [DefaultValue("0011223344556677")]
        public string Data { get; set; }
        /// <summary>
        /// Hashing Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue("sha256")]
        public string Algorithm { get; set; }

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
