using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class Experience
    {
        public int Id { get; set; }
        public string Employer { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ReasonForLeaving { get; set; }
        public string Description { get; set; }
        public bool? PreviousMunicipality { get; set; }
        public string PreviousMunicipalityName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
