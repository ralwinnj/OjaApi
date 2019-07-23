using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BcmmOja.Models
{
    public partial class bcmm_ojaContext : DbContext
    {
        public bcmm_ojaContext()
        {
        }

        public bcmm_ojaContext(DbContextOptions<bcmm_ojaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicant { get; set; }
        public virtual DbSet<ApplicantDocument> ApplicantDocument { get; set; }
        public virtual DbSet<ApplicantVacancy> ApplicantVacancy { get; set; }
        public virtual DbSet<ComputerLiteracy> ComputerLiteracy { get; set; }
        public virtual DbSet<CriminalRecord> CriminalRecord { get; set; }
        public virtual DbSet<DisciplinaryRecord> DisciplinaryRecord { get; set; }
        public virtual DbSet<Experience> Experience { get; set; }
        public virtual DbSet<General> General { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LoginLog> LoginLog { get; set; }
        public virtual DbSet<PoliticalOffice> PoliticalOffice { get; set; }
        public virtual DbSet<ProfessionalMembership> ProfessionalMembership { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<Reference> Reference { get; set; }
        public virtual DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

//                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer("Data Source=W001942;Initial Catalog=bcmm_oja;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.ToTable("applicant");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__applican__3213E83EF300806A")
                    .IsUnique();

                entity.HasIndex(e => e.IdNumber)
                    .HasName("UQ__applican__D58CDE11D64F9B8C")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber)
                    .HasName("UQ__applican__A1936A6BB2B5D386")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Citizenship).HasColumnName("citizenship");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Dependant).HasColumnName("dependant");

                entity.Property(e => e.DependantAge)
                    .HasColumnName("dependant_age")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Disability).HasColumnName("disability");

                entity.Property(e => e.DisabilityNature)
                    .HasColumnName("disability_nature")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DriversLicence).HasColumnName("drivers_licence");

                entity.Property(e => e.DriversLicenceType)
                    .HasColumnName("drivers_licence_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdNumber)
                    .HasColumnName("id_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasColumnName("nationality")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SarsRegistered).HasColumnName("sars_registered");

                entity.Property(e => e.SarsTaxNumber)
                    .HasColumnName("sars_tax-number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.WorkPermitNumber)
                    .HasColumnName("work_permit_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApplicantDocument>(entity =>
            {
                entity.ToTable("applicant_document");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DocumentFormat)
                    .HasColumnName("document_format")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentName)
                    .HasColumnName("document_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentPath)
                    .HasColumnName("document_path")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentType)
                    .HasColumnName("document_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");
            });

            modelBuilder.Entity<ApplicantVacancy>(entity =>
            {
                entity.ToTable("applicant_vacancy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingDate).HasColumnName("closing_date");

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.Directorate)
                    .HasColumnName("directorate")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Download)
                    .HasColumnName("download")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Kpas)
                    .HasColumnName("kpas")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Package)
                    .HasColumnName("package")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Requirements)
                    .HasColumnName("requirements")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.ApplicantVacancy)
                    .HasForeignKey(d => d.FkApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__applicant__fk_ap__6E01572D");
            });

            modelBuilder.Entity<ComputerLiteracy>(entity =>
            {
                entity.ToTable("computer_literacy");

                entity.HasIndex(e => e.FkApplicantId)
                    .HasName("UQ__computer__44609BBDE95DD8F6")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__computer__3213E83E19B61E6A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Competency)
                    .HasColumnName("competency")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.Skill)
                    .HasColumnName("skill")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithOne(p => p.ComputerLiteracy)
                    .HasForeignKey<ComputerLiteracy>(d => d.FkApplicantId)
                    .HasConstraintName("FK__computer___fk_ap__70DDC3D8");
            });

            modelBuilder.Entity<CriminalRecord>(entity =>
            {
                entity.ToTable("criminal_record");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__criminal__3213E83E8DB32771")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DateFinalized)
                    .HasColumnName("date_finalized")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.Outcome)
                    .HasColumnName("outcome")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Record).HasColumnName("record");

                entity.Property(e => e.TypeOfCriminalAct)
                    .HasColumnName("type_of_criminal_act")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.CriminalRecord)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__criminal___fk_ap__73BA3083");
            });

            modelBuilder.Entity<DisciplinaryRecord>(entity =>
            {
                entity.ToTable("disciplinary_record");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__discipli__3213E83E8D82E3E3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AwardSanction)
                    .HasColumnName("award_sanction")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.DateFinalized).HasColumnName("date_finalized");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.NameOfInstitute)
                    .HasColumnName("name_of_institute")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Record).HasColumnName("record");

                entity.Property(e => e.Resign).HasColumnName("resign");

                entity.Property(e => e.ResignReason)
                    .HasColumnName("resign_reason")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfMisconduct)
                    .HasColumnName("type_of_misconduct")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.DisciplinaryRecord)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__disciplin__fk_ap__76969D2E");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("experience");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__experien__3213E83E1F6A3D9E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Employer)
                    .HasColumnName("employer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousMunicipality).HasColumnName("previous_municipality");

                entity.Property(e => e.PreviousMunicipalityName)
                    .HasColumnName("previous_municipality_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonForLeaving)
                    .HasColumnName("reason_for_leaving")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.Experience)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__experienc__fk_ap__797309D9");
            });

            modelBuilder.Entity<General>(entity =>
            {
                entity.ToTable("general");

                entity.HasIndex(e => e.FkApplicantId)
                    .HasName("UQ__general__44609BBDB61747B4")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommenceDate).HasColumnName("commence_date");

                entity.Property(e => e.ConflictOfInterest).HasColumnName("conflict_of_interest");

                entity.Property(e => e.ConflictOfInterestReason)
                    .HasColumnName("conflict_of_interest_reason")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.PhysicalMentalCondition).HasColumnName("physical_mental_condition");

                entity.Property(e => e.PositionTermsAccepted).HasColumnName("position_terms_accepted");

                entity.HasOne(d => d.FkApplicant)
                    .WithOne(p => p.General)
                    .HasForeignKey<General>(d => d.FkApplicantId)
                    .HasConstraintName("FK__general__fk_appl__7C4F7684");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__login__AB6E6164F4E838FD")
                    .IsUnique();

                entity.HasIndex(e => e.FkApplicantId)
                    .HasName("UQ__login__44609BBD1B8A164F")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__login__3213E83E8BB91BE9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.FkApplicantId)
                    .HasConstraintName("FK__login__fk_applic__7F2BE32F");
            });

            modelBuilder.Entity<LoginLog>(entity =>
            {
                entity.ToTable("login_log");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__login_lo__3213E83E1216859D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.LoginLog)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__login_log__fk_ap__02084FDA");
            });

            modelBuilder.Entity<PoliticalOffice>(entity =>
            {
                entity.ToTable("political_office");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__politica__3213E83EDC457CD5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.PoliticalOffice1).HasColumnName("political_office");

                entity.Property(e => e.PoliticalParty)
                    .HasColumnName("political_party")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.PoliticalOffice)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__political__fk_ap__04E4BC85");
            });

            modelBuilder.Entity<ProfessionalMembership>(entity =>
            {
                entity.ToTable("professional_membership");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.MembershipNumber)
                    .HasColumnName("membership_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfessionalBody)
                    .HasColumnName("professional_body")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.ProfessionalMembership)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__professio__fk_ap__07C12930");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("qualification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.NameOfInstitute)
                    .HasColumnName("name_of_institute")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfQualification)
                    .HasColumnName("name_of_qualification")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfQualification)
                    .HasColumnName("type_of_qualification")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YearObtained).HasColumnName("year_obtained");

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.Qualification)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__qualifica__fk_ap__0A9D95DB");
            });

            modelBuilder.Entity<Reference>(entity =>
            {
                entity.ToTable("reference");

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__referenc__3213E83E947BF04C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CellNumber)
                    .HasColumnName("cell_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkApplicantId).HasColumnName("fk_applicant_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Relationship)
                    .HasColumnName("relationship")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelNumber)
                    .HasColumnName("tel_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkApplicant)
                    .WithMany(p => p.Reference)
                    .HasForeignKey(d => d.FkApplicantId)
                    .HasConstraintName("FK__reference__fk_ap__0D7A0286");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.ToTable("vacancy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingDate).HasColumnName("closing_date");

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.Directorate)
                    .HasColumnName("directorate")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Download)
                    .HasColumnName("download")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Kpas)
                    .HasColumnName("kpas")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Package)
                    .HasColumnName("package")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Requirements)
                    .HasColumnName("requirements")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnName("year");
            });
        }
    }
}
