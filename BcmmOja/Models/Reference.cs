using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class Reference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string TelNumber { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
