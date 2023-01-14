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
        /// Plain Data in Hex Format
        /// </summary>
        [Required]
        [DefaultValue("0011223344556677")]
        public string HexData { get; set; }
        /// <summary>
        /// Hashing Algorithm to use
        /// </summary>
        [Required]
        [DefaultValue(HashSupportedAlgorithms.sha256)]
        public HashSupportedAlgorithms Algorithm { get; set; }
    }
}
