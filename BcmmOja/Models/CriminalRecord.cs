using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class CriminalRecord
    {
        public int Id { get; set; }
        public bool Record { get; set; }
        public string TypeOfCriminalAct { get; set; }
        public string DateFinalized { get; set; }
        public string Outcome { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
