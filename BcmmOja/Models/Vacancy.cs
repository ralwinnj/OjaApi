using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Directorate { get; set; }
        public string Grade { get; set; }
        public string Package { get; set; }
        public string Reference { get; set; }
        public string Requirements { get; set; }
        public string Kpas { get; set; }
        public short? Date { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Download { get; set; }
        public string Contact { get; set; }
        public string Author { get; set; }
        public string Active { get; set; }
        public short? Count { get; set; }
        public short? Day { get; set; }
        public byte? Month { get; set; }
        public short? Year { get; set; }
    }
}
