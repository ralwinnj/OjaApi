using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class DisciplinaryRecord
    {
        public int Id { get; set; }
        public bool Record { get; set; }
        public string NameOfInstitute { get; set; }
        public string TypeOfMisconduct { get; set; }
        public DateTime DateFinalized { get; set; }
        public string AwardSanction { get; set; }
        public bool Resign { get; set; }
        public string ResignReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
