using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Mac.Verify
{
    public class VerifyResponse
    {
        [Required]
        [DefaultValue(true)]
        public bool IsVerified { get; set; }

        [Required]
        [DefaultValue(11)]
        public long ProcessingTimeInMs { get; set; }
    }
}
