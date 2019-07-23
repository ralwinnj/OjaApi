using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BcmmOja.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
