using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;
using VMD.RESTApiResponseWrapper.Core.Wrappers;
using VMD.RESTApiResponseWrapper.Core.Extensions;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;


        public ApplicantsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/Applicants
        [HttpGet]
        public APIResponse GetApplicant()
        {
            try
            {
                return new APIResponse(200, $"Success", _context.Applicant);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server Error!", ex.Message);
            }
        }

        // GET: api/Applicants/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetApplicant([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error.", ModelStateExtension.AllErrors(ModelState));
                }

                var applicant = await _context.Applicant.FindAsync(id);

                if (applicant == null)
                {
                    return new APIResponse(404, $"Could not find applicant with id of {id}.");
                }

                var computerLiteracy = await _context.ComputerLiteracy
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Skill, x.Competency, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var login = await _context.Login
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Email, x.LastLogin, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var applicantDocument = await _context.ApplicantDocument
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Document, x.DocumentName, x.DocumentPath, x.DocumentFormat, x.DocumentType, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var criminalRecord = await _context.CriminalRecord
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Record, x.TypeOfCriminalAct, x.DateFinalized, x.Outcome, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var disciplinaryRecord = await _context.DisciplinaryRecord
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Record, x.NameOfInstitute, x.TypeOfMisconduct, x.DateFinalized, x.AwardSanction, x.Resign, x.ResignReason, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var experience = await _context.Experience
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Employer, x.Position, x.StartDate, x.EndDate, x.ReasonForLeaving, x.Description, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var professionalMembership = await _context.ProfessionalMembership
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.ProfessionalBody, x.MembershipNumber, x.ExpiryDate, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var qualification = await _context.Qualification
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.NameOfInstitute, x.NameOfQualification, x.TypeOfQualification, x.YearObtained, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var Reference = await _context.Reference
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Name, x.Relationship, x.TelNumber, x.CellNumber, x.Email, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var general = await _context.General
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.PhysicalMentalCondition, x.ConflictOfInterest, x.ConflictOfInterestReason, x.CommenceDate, x.PositionTermsAccepted, x.CreatedAt, x.FkApplicantId })
                    .ToListAsync();

                var applicantVacancy = await _context.ApplicantVacancy
                    .Where(x => x.FkApplicantId == id)
                    .Select(x => new { x.Id, x.Title, x.Directorate, x.Grade, x.Package, x.Reference, x.Requirements, x.Kpas, x.ClosingDate, x.Download, x.Contact, x.Author, x.Active, x.FkApplicant })
                    .ToListAsync();

                dynamic myApplicant = new JObject();
                myApplicant.id = applicant.Id;
                myApplicant.title = applicant.Title;
                myApplicant.firstName = applicant.FirstName;
                myApplicant.lastName = applicant.LastName;
                myApplicant.gender = applicant.Gender;
                myApplicant.race = applicant.Race;

                myApplicant.dependant = applicant.Dependant;
                myApplicant.dependantAge = applicant.DependantAge;
                myApplicant.citizenship = applicant.Citizenship;
                myApplicant.idNumber = applicant.IdNumber;
                myApplicant.nationality = applicant.Nationality;
                myApplicant.workPermitNumber = applicant.WorkPermitNumber;
                myApplicant.sarsRegistered = applicant.SarsRegistered;
                myApplicant.sarsTaxNumber = applicant.SarsTaxNumber;
                myApplicant.driversLicence = applicant.DriversLicence;
                myApplicant.driversLicenceType = applicant.DriversLicenceType;
                myApplicant.address = applicant.Address;
                myApplicant.language = applicant.Language;
                myApplicant.phoneNumber = applicant.PhoneNumber;
                myApplicant.natureOfEmployment = applicant.NatureOfEmployment;
                myApplicant.relationship = applicant.Relationship;
                myApplicant.heardAboutUs = applicant.HeardAboutUs;
                myApplicant.marketingInfo = applicant.MarketingInfo;
                myApplicant.birthDate = applicant.BirthDate;

                var data = new object[1];

                data[0] = new
                {
                    applicant = applicant,
                    login = login,
                    general = general,
                    applicantVacancy = applicantVacancy,
                    computerLiteracy = computerLiteracy,
                    criminalRecord = criminalRecord,
                    disciplinaryRecord = disciplinaryRecord,
                    experience = experience,
                    professionalMembership = professionalMembership,
                    qualification = qualification,
                    Reference = Reference,
                };

                return new APIResponse(200, $"Applicant found.", data);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server Error!", ex.Message);
            }
        }

        // PUT: api/Applicants/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutApplicant([FromRoute] int id, [FromBody] Applicant applicant)
        {
            try
            {
                if (!ApplicantExists(id))
                {
                    return new APIResponse(404, $"Applicant not found!", id);
                };

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error!", ModelStateExtension.AllErrors(ModelState));
                };

                if (id != applicant.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {applicant.Id}.", ModelStateExtension.AllErrors(ModelState));
                };

                _context.Entry(applicant).State = EntityState.Modified;

                await _context.SaveChangesAsync();


                return new APIResponse(200, $"Applicant details updated successfully.", applicant);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server Error!", ex.Message);
            }
        }

        // POST: api/Applicants
        [HttpPost]
        public async Task<APIResponse> PostApplicant([FromBody] Applicant applicant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation error.", ModelStateExtension.AllErrors(ModelState));
                }
                if (await _context.Applicant.AnyAsync(x => x.IdNumber == applicant.IdNumber))
                {
                    return new APIResponse(300, $"A user already exists with the ID number {applicant.IdNumber}.", applicant);
                };
                if (await _context.Applicant.AnyAsync(x => x.PhoneNumber == applicant.PhoneNumber))
                {
                    return new APIResponse(300, $"A user already exists with the phone number {applicant.PhoneNumber}.", applicant);
                }

                if (applicant.Login != null)
                {
                    if (await _context.Login.AnyAsync(x => x.Email == applicant.Login.Email))
                    {
                        return new APIResponse(300, $"A user already exists with the email address {applicant.Login.Email}", applicant);
                    }
                }

                var aData = new Applicant()
                {
                    Title = applicant.Title,
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,
                    Race = applicant.Race,
                    Dependant = applicant.Dependant,
                    DependantAge = applicant.DependantAge,
                    Disability = applicant.Disability,
                    DisabilityNature = applicant.DisabilityNature,
                    Citizenship = applicant.Citizenship,
                    IdNumber = applicant.IdNumber,
                    Nationality = applicant.Nationality,
                    WorkPermitNumber = applicant.WorkPermitNumber,
                    SarsRegistered = applicant.SarsRegistered,
                    SarsTaxNumber = applicant.SarsTaxNumber,
                    DriversLicence = applicant.DriversLicence,
                    DriversLicenceType = applicant.DriversLicenceType,
                    Address = applicant.Address,
                    Language = applicant.Language,
                    PhoneNumber = applicant.PhoneNumber,
                    CreatedAt = applicant.CreatedAt,
                };

                await _context.Applicant.AddAsync(aData);


                if (applicant.Login != null)
                {
                    var a = _context.Login.FirstOrDefault(x => x.FkApplicantId == applicant.Id);
                    a.Email = applicant.Login.Email;
                    await _context.Login.AddAsync(a);
                };

                //if (applicant.ApplicantDocument != null)
                //{
                //    foreach (ApplicantDocument el in applicant.ApplicantDocument)
                //    {
                //        var b = new ApplicantDocument()
                //        {
                //            Document = el.Document,
                //            DocumentFormat = el.DocumentFormat,
                //            DocumentName = el.DocumentName,
                //            DocumentPath = el.DocumentPath,
                //            DocumentType = el.DocumentType,
                //            CreatedAt = new DateTime(),
                //            FkApplicantId = applicant.Id
                //        };
                //        await _context.ApplicantDocument.AddAsync(b);
                //    }
                // };

                if (applicant.CriminalRecord != null)
                {

                    foreach (CriminalRecord el in applicant.CriminalRecord)
                    {
                        var b = new CriminalRecord()
                        {
                            Record = el.Record || false,
                            TypeOfCriminalAct = el.TypeOfCriminalAct,
                            DateFinalized = el.DateFinalized,
                            Outcome = el.Outcome,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.CriminalRecord.AddAsync(b);
                    }
                };

                if (applicant.DisciplinaryRecord != null)
                {
                    foreach (DisciplinaryRecord el in applicant.DisciplinaryRecord)
                    {
                        var b = new DisciplinaryRecord()
                        {
                            Record = el.Record || false,
                            NameOfInstitute = el.NameOfInstitute,
                            TypeOfMisconduct = el.TypeOfMisconduct,
                            DateFinalized = el.DateFinalized,
                            AwardSanction = el.AwardSanction,
                            Resign = el.Resign || false,
                            ResignReason = el.ResignReason,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.DisciplinaryRecord.AddAsync(b);
                    }
                };

                if (applicant.Experience != null)
                {
                    foreach (Experience el in applicant.Experience)
                    {
                        var b = new Experience()
                        {
                            Employer = el.Employer,
                            Position = el.Position,
                            StartDate = el.StartDate,
                            EndDate = el.EndDate,
                            ReasonForLeaving = el.ReasonForLeaving,
                            Description = el.Description,
                            PreviousMunicipality = el.PreviousMunicipality || false,
                            PreviousMunicipalityName = el.PreviousMunicipalityName,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.Experience.AddAsync(b);
                    }
                };

                if (applicant.PoliticalOffice != null)
                {
                    foreach (PoliticalOffice el in applicant.PoliticalOffice)
                    {
                        var b = new PoliticalOffice()
                        {
                            PoliticalOffice1 = el.PoliticalOffice1 || false,
                            PoliticalParty = el.PoliticalParty,
                            Position = el.Position,
                            ExpiryDate = el.ExpiryDate,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.PoliticalOffice.AddAsync(b);
                    }
                };

                if (applicant.ProfessionalMembership != null)
                {
                    foreach (ProfessionalMembership el in applicant.ProfessionalMembership)
                    {
                        var b = new ProfessionalMembership()
                        {
                            ProfessionalBody = el.ProfessionalBody,
                            MembershipNumber = el.MembershipNumber,
                            ExpiryDate = el.ExpiryDate,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.ProfessionalMembership.AddAsync(b);
                    }
                };

                if (applicant.Qualification != null)
                {
                    foreach (Qualification el in applicant.Qualification)
                    {
                        var b = new Qualification()
                        {
                            NameOfInstitute = el.NameOfInstitute,
                            NameOfQualification = el.NameOfQualification,
                            TypeOfQualification = el.TypeOfQualification,
                            YearObtained = el.YearObtained,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.Qualification.AddAsync(b);
                    }
                };

                if (applicant.Reference != null)
                {
                    foreach (Reference el in applicant.Reference)
                    {
                        var b = new Reference()
                        {
                            Name = el.Name,
                            Relationship = el.Name,
                            TelNumber = el.TelNumber ?? "",
                            CellNumber = el.CellNumber,
                            Email = el.Email,
                            CreatedAt = new DateTime(),
                            FkApplicantId = applicant.Id
                        };
                        await _context.Reference.AddAsync(b);
                    }
                };
                await _context.SaveChangesAsync();
                return new APIResponse(200, "User created successfully", applicant);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }
        }

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteApplicant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new APIResponse(400, "Validation error.", ModelStateExtension.AllErrors(ModelState));
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return new APIResponse(404, $"Applicant with id {id}, not found.");
            }

            var login = await _context.Login.FirstAsync(x => x.FkApplicantId == id);
            if (login != null)
            {
                _context.Login.Remove(login);
            }

            _context.Applicant.Remove(applicant);

            await _context.SaveChangesAsync();

            return new APIResponse(200, $"User successfully deleted", applicant);
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.Id == id);
        }
    }
}