using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class Qualification
    {
        public int Id { get; set; }
        public string NameOfInstitute { get; set; }
        public string NameOfQualification { get; set; }
        public string TypeOfQualification { get; set; }
        public int? YearObtained { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
