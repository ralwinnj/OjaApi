using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class General
    {
        public int Id { get; set; }
        public bool PhysicalMentalCondition { get; set; }
        public bool ConflictOfInterest { get; set; }
        public string ConflictOfInterestReason { get; set; }
        public DateTime CommenceDate { get; set; }
        public bool PositionTermsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
