using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BcmmOja.Migrations
{
    public partial class UpdateDBSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicant",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    first_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    gender = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    race = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    dependant = table.Column<bool>(nullable: true),
                    dependant_age = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    disability = table.Column<bool>(nullable: true),
                    disability_nature = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    citizenship = table.Column<bool>(nullable: false),
                    id_number = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    nationality = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    work_permit_number = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    sars_registered = table.Column<bool>(nullable: true),
                    sars_taxnumber = table.Column<string>(name: "sars_tax-number", unicode: false, maxLength: 20, nullable: true),
                    drivers_licence = table.Column<bool>(nullable: true),
                    drivers_licence_type = table.Column<string>(unicode: false, nullable: true),
                    address = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    language = table.Column<string>(unicode: false, nullable: true),
                    phone_number = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    nature_of_employment = table.Column<string>(unicode: false, nullable: true),
                    relationship = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    languages = table.Column<string>(unicode: false, nullable: true),
                    heard_about_us = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    marketing_info = table.Column<bool>(nullable: true),
                    birth_date = table.Column<DateTime>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vacancy",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    directorate = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    grade = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    package = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    reference = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    requirements = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    kpas = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    date = table.Column<short>(nullable: true),
                    closing_date = table.Column<DateTime>(nullable: true),
                    download = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    contact = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    author = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    active = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    count = table.Column<short>(nullable: true),
                    day = table.Column<short>(nullable: true),
                    month = table.Column<byte>(nullable: true),
                    year = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicant_document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    document = table.Column<byte[]>(nullable: false),
                    document_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    document_path = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    document_format = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    document_type = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicant_document", x => x.id);
                    table.ForeignKey(
                        name: "FK__applicant__fk_ap__6D0D32F4",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicant_vacancy",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    directorate = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    grade = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    package = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    reference = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    requirements = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    kpas = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    date = table.Column<short>(nullable: true),
                    closing_date = table.Column<DateTime>(nullable: true),
                    download = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    contact = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    author = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    active = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    count = table.Column<short>(nullable: true),
                    day = table.Column<short>(nullable: true),
                    month = table.Column<byte>(nullable: true),
                    year = table.Column<short>(nullable: true),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicant_vacancy", x => x.id);
                    table.ForeignKey(
                        name: "FK__applicant__fk_ap__6E01572D",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "computer_literacy",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    skill = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    competency = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computer_literacy", x => x.id);
                    table.ForeignKey(
                        name: "FK__computer___fk_ap__6EF57B66",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "criminal_record",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    record = table.Column<bool>(nullable: false),
                    type_of_criminal_act = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    date_finalized = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    outcome = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_criminal_record", x => x.id);
                    table.ForeignKey(
                        name: "FK__criminal___fk_ap__6FE99F9F",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "disciplinary_record",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    record = table.Column<bool>(nullable: false),
                    name_of_institute = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    type_of_misconduct = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    date_finalized = table.Column<DateTime>(nullable: false),
                    award_sanction = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    resign = table.Column<bool>(nullable: false),
                    resign_reason = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplinary_record", x => x.id);
                    table.ForeignKey(
                        name: "FK__disciplin__fk_ap__70DDC3D8",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "experience",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    employer = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    position = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: true),
                    reason_for_leaving = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    previous_municipality = table.Column<bool>(nullable: false),
                    previous_municipality_name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_experience", x => x.id);
                    table.ForeignKey(
                        name: "FK__experienc__fk_ap__71D1E811",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "general",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    physical_mental_condition = table.Column<bool>(nullable: false),
                    conflict_of_interest = table.Column<bool>(nullable: false),
                    conflict_of_interest_reason = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    commence_date = table.Column<DateTime>(nullable: false),
                    position_terms_accepted = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general", x => x.id);
                    table.ForeignKey(
                        name: "FK__general__fk_appl__72C60C4A",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    last_login = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.id);
                    table.ForeignKey(
                        name: "FK__login__fk_applic__73BA3083",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "login_log",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_log", x => x.id);
                    table.ForeignKey(
                        name: "FK__login_log__fk_ap__74AE54BC",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "political_office",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    political_office = table.Column<bool>(nullable: false),
                    political_party = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    position = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_political_office", x => x.id);
                    table.ForeignKey(
                        name: "FK__political__fk_ap__75A278F5",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "professional_membership",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    professional_body = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    membership_number = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professional_membership", x => x.id);
                    table.ForeignKey(
                        name: "FK__professio__fk_ap__76969D2E",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qualification",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name_of_institute = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    name_of_qualification = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    type_of_qualification = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    year_obtained = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualification", x => x.id);
                    table.ForeignKey(
                        name: "FK__qualifica__fk_ap__778AC167",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reference",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    relationship = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    tel_number = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    cell_number = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "(sysdatetime())"),
                    fk_applicant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reference", x => x.id);
                    table.ForeignKey(
                        name: "FK__reference__fk_ap__787EE5A0",
                        column: x => x.fk_applicant_id,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__applican__3213E83ED6610A03",
                table: "applicant",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__applican__D58CDE11582859F6",
                table: "applicant",
                column: "id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__applican__A1936A6B66B20B1F",
                table: "applicant",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicant_document_fk_applicant_id",
                table: "applicant_document",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__applican__3213E83E2999B4E4",
                table: "applicant_document",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicant_vacancy_fk_applicant_id",
                table: "applicant_vacancy",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_computer_literacy_fk_applicant_id",
                table: "computer_literacy",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__computer__3213E83E4979668E",
                table: "computer_literacy",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_criminal_record_fk_applicant_id",
                table: "criminal_record",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__criminal__3213E83E59D6934C",
                table: "criminal_record",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_disciplinary_record_fk_applicant_id",
                table: "disciplinary_record",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__discipli__3213E83EAB458416",
                table: "disciplinary_record",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_experience_fk_applicant_id",
                table: "experience",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__experien__3213E83E5807A6D4",
                table: "experience",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__general__44609BBD0D027BA3",
                table: "general",
                column: "fk_applicant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__login__AB6E6164EDA9114A",
                table: "login",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__login__44609BBD75A61BF0",
                table: "login",
                column: "fk_applicant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__login__3213E83E44A7BD22",
                table: "login",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_login_log_fk_applicant_id",
                table: "login_log",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__login_lo__3213E83ECCEF71D5",
                table: "login_log",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_political_office_fk_applicant_id",
                table: "political_office",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__politica__3213E83E789AD027",
                table: "political_office",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_professional_membership_fk_applicant_id",
                table: "professional_membership",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_qualification_fk_applicant_id",
                table: "qualification",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_reference_fk_applicant_id",
                table: "reference",
                column: "fk_applicant_id");

            migrationBuilder.CreateIndex(
                name: "UQ__referenc__3213E83E020F90F9",
                table: "reference",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicant_document");

            migrationBuilder.DropTable(
                name: "applicant_vacancy");

            migrationBuilder.DropTable(
                name: "computer_literacy");

            migrationBuilder.DropTable(
                name: "criminal_record");

            migrationBuilder.DropTable(
                name: "disciplinary_record");

            migrationBuilder.DropTable(
                name: "experience");

            migrationBuilder.DropTable(
                name: "general");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "login_log");

            migrationBuilder.DropTable(
                name: "political_office");

            migrationBuilder.DropTable(
                name: "professional_membership");

            migrationBuilder.DropTable(
                name: "qualification");

            migrationBuilder.DropTable(
                name: "reference");

            migrationBuilder.DropTable(
                name: "vacancy");

            migrationBuilder.DropTable(
                name: "applicant");
        }
    }
}
