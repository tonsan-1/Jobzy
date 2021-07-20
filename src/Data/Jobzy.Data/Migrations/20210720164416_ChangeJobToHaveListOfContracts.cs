using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class ChangeJobToHaveListOfContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Contracts_ContractId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ContractId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "JobId",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobId",
                table: "Contracts",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Jobs_JobId",
                table: "Contracts",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Jobs_JobId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_JobId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "ContractId",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ContractId",
                table: "Jobs",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Contracts_ContractId",
                table: "Jobs",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
