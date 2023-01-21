using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Mac.Sign
{
    /// <summary>
    /// Signing Response Object
    /// </summary>
    public class SignResponse
    {
        /// <summary>
        /// Message Authentication Code Value
        /// </summary>
        [Required]
        [DefaultValue("F8409911928AEBF52A0C3A88AABE16A6")]
        public string Mac { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue(11)]
        public long ProcessingTimeInMs { get; set; }
    }
}
