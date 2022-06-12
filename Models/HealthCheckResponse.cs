using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models
{
    /// <summary>
    /// Healthcheck Response Object
    /// </summary>
    public class HealthCheckResponse
    {
        /// <summary>
        /// Service Status
        /// </summary>
        [Required]
        [DefaultValue("Iam Healthy")]
        public string Status { get; set; }
        /// <summary>
        /// Request Processing Time in ms
        /// </summary>
        [Required]
        [DefaultValue("11 ms")]
        public string ProcessingTimeInMs { get; set; }
    }
}
