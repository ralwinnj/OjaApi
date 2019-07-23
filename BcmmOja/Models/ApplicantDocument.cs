using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BcmmOja.Models
{
    public partial class ApplicantDocument
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name of the document is required!")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "The file path of the document is required!")]
        public string DocumentPath { get; set; }

        [Required(ErrorMessage = "The file format of the document is required!")]
        public string DocumentFormat { get; set; }

        [Required(ErrorMessage = "The document type of the document is required!")]
        public string DocumentType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? FkApplicantId { get; set; }
    }
}
