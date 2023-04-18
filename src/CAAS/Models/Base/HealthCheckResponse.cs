using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Base
{

    public class HealthCheckResponse
    {

        [Required]
        [DefaultValue("Iam Healthy")]
        public string Status { get; set; }

        [Required]
        [DefaultValue(11)]
        public double ProcessingTimeInMs { get; set; }
    }
}
