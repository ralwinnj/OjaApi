using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BcmmOja.Models
{
    public partial class Otp
    {
        public int Id { get; set; }
        [Required]
        public string OtpSentVia { get; set; }
        public string OtpSentValue { get; set; }
        public bool OtpSentVerified { get; set; }
        [Required]
        public int OtpSentToId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public partial class OtpPut
    {
        public string OtpReceivedValue { get; set; }
    }
}
