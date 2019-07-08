using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
