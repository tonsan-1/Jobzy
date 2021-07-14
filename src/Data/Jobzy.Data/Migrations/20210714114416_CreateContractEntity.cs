using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class CreateContractEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmoployerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployerId",
                table: "Contracts",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FreelancerId",
                table: "Contracts",
                column: "FreelancerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "HourlyRate",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
