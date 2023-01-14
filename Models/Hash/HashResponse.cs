using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Hash
{
    /// <summary>
    /// Hash Response Object
    /// </summary>
    public class HashResponse
    {
        /// <summary>
        /// Hashed Data in Hex Format
        /// </summary>
        [Required]
        [DefaultValue("d1a5f998fa6ed82da6943127533b412f2286b30c8473a819f70a8fec5913fea7")]
        public string HexHashData { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("11 ms")]
        public string ProcessingTimeInMs { get; set; }
    }
}
