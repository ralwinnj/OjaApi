using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class ComputerLiteracy
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public string Competency { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
