using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Rng
{
    /// <summary>
    /// Random Number Generator Response Object
    /// </summary>
    public class RngResponse
    {
        /// <summary>
        /// Random Number Generated
        /// </summary>
        [Required]
        [DefaultValue("00112233445566770011223344556677")]
        public string Rng { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue(11)]
        public long ProcessingTimeInMs { get; set; }
    }
}
