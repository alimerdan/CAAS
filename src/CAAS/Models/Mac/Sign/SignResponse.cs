using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Mac.Sign
{

    public class SignResponse
    {

        [Required]
        [DefaultValue("F8409911928AEBF52A0C3A88AABE16A6")]
        public string Mac { get; set; }

        [Required]
        [DefaultValue(11)]
        public double ProcessingTimeInMs { get; set; }
    }
}
