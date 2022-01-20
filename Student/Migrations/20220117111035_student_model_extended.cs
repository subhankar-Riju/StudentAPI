using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Migrations
{
    public partial class student_model_extended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FathersName",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mothersname",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "students");

            migrationBuilder.DropColumn(
                name: "City",
                table: "students");

            migrationBuilder.DropColumn(
                name: "FathersName",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Mothersname",
                table: "students");
        }
    }
}
