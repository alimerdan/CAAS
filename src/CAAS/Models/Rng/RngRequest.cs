using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Rng
{
    /// <summary>
    /// Random Number Generator Request Object
    /// </summary>
    public class RngRequest
    {

        /// <summary>
        /// Generation Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue("csprng")]
        public string Algorithm { get; set; }

        /// <summary>
        /// Random Number Size
        /// </summary>
        [Required]
        [DefaultValue(16)]
        public int Size { get; set; }

        /// <summary>
        /// Required output data format (hex, base64, ascii or utf8)
        /// </summary>
        [Required]
        [DefaultValue("hex")]
        public string OutputDataFormat { get; set; }
    }
}
