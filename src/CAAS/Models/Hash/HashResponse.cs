﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CAAS.Models.Hash
{

    public class HashResponse
    {

        [Required]
        [DefaultValue("d1a5f998fa6ed82da6943127533b412f2286b30c8473a819f70a8fec5913fea7")]
        public string Digest { get; set; }

        [Required]
        [DefaultValue(11)]
        public double ProcessingTimeInMs { get; set; }
    }
}
