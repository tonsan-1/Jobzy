using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class AddOfferToContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferId",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OfferId",
                table: "Contracts",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Offers_OfferId",
                table: "Contracts",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Offers_OfferId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_OfferId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Contracts");
        }
    }
}
