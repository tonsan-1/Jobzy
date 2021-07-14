using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class FixEmployerIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmoployerId",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmoployerId",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
