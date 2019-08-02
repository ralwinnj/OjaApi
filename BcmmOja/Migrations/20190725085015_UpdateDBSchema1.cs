using Microsoft.EntityFrameworkCore.Migrations;

namespace BcmmOja.Migrations
{
    public partial class UpdateDBSchema1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "marketing_info",
                table: "applicant",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "marketing_info",
                table: "applicant",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
