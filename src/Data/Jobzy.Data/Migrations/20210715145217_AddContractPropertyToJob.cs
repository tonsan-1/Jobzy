using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class AddContractPropertyToJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
