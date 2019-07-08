using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class ApplicantDocument
    {
        public int Id { get; set; }
        public byte[] Document { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentFormat { get; set; }
        public string DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FkApplicantId { get; set; }

        public Applicant FkApplicant { get; set; }
    }
}
