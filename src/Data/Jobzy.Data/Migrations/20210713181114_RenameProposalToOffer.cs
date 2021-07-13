using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class RenameProposalToOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FixedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDays = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_AspNetUsers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_FreelancerId",
                table: "Offers",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_JobId",
                table: "Offers",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryDays = table.Column<int>(type: "int", nullable: false),
                    FixedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_AspNetUsers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposals_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_FreelancerId",
                table: "Proposals",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobId",
                table: "Proposals",
                column: "JobId");
        }
    }
}
