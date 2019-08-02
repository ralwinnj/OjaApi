using System;
using System.Collections.Generic;

namespace BcmmOja.Models
{
    public partial class Applicant
    {
        public Applicant()
        {
            ApplicantDocument = new HashSet<ApplicantDocument>();
            ApplicantVacancy = new HashSet<ApplicantVacancy>();
            ComputerLiteracy = new HashSet<ComputerLiteracy>();
            CriminalRecord = new HashSet<CriminalRecord>();
            DisciplinaryRecord = new HashSet<DisciplinaryRecord>();
            Experience = new HashSet<Experience>();
            LoginLog = new HashSet<LoginLog>();
            PoliticalOffice = new HashSet<PoliticalOffice>();
            ProfessionalMembership = new HashSet<ProfessionalMembership>();
            Qualification = new HashSet<Qualification>();
            Reference = new HashSet<Reference>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public bool? Dependant { get; set; }
        public string DependantAge { get; set; }
        public bool? Disability { get; set; }
        public string DisabilityNature { get; set; }
        public bool Citizenship { get; set; }
        public string IdNumber { get; set; }
        public string Nationality { get; set; }
        public string WorkPermitNumber { get; set; }
        public bool? SarsRegistered { get; set; }
        public string SarsTaxNumber { get; set; }
        public bool? DriversLicence { get; set; }
        public string DriversLicenceType { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public string PhoneNumber { get; set; }
        public string NatureOfEmployment { get; set; }
        public string Relationship { get; set; }
        public string Languages { get; set; }
        public string HeardAboutUs { get; set; }
        public bool? MarketingInfo { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public General General { get; set; }
        public Login Login { get; set; }
        public ICollection<ApplicantDocument> ApplicantDocument { get; set; }
        public ICollection<ApplicantVacancy> ApplicantVacancy { get; set; }
        public ICollection<ComputerLiteracy> ComputerLiteracy { get; set; }
        public ICollection<CriminalRecord> CriminalRecord { get; set; }
        public ICollection<DisciplinaryRecord> DisciplinaryRecord { get; set; }
        public ICollection<Experience> Experience { get; set; }
        public ICollection<LoginLog> LoginLog { get; set; }
        public ICollection<PoliticalOffice> PoliticalOffice { get; set; }
        public ICollection<ProfessionalMembership> ProfessionalMembership { get; set; }
        public ICollection<Qualification> Qualification { get; set; }
        public ICollection<Reference> Reference { get; set; }
    }
}
