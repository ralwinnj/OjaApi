using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class PoliticalOffice
    {
        public int Id { get; set; }
        public bool PoliticalOffice1 { get; set; }
        public string PoliticalParty { get; set; }
        public string Position { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
