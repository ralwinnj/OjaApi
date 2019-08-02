using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class LoginLog
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
