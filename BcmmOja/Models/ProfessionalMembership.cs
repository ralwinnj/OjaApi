using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class ProfessionalMembership
    {
        public int Id { get; set; }
        public string ProfessionalBody { get; set; }
        public string MembershipNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
