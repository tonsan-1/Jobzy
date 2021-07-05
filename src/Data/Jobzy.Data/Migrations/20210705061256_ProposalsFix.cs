using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobzy.Data.Migrations
{
    public partial class ProposalsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_Jobs_JobId",
                table: "Proposal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proposal",
                table: "Proposal");

            migrationBuilder.RenameTable(
                name: "Proposal",
                newName: "Proposals");

            migrationBuilder.RenameIndex(
                name: "IX_Proposal_JobId",
                table: "Proposals",
                newName: "IX_Proposals_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposal_FreelancerId",
                table: "Proposals",
                newName: "IX_Proposals_FreelancerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proposals",
                table: "Proposals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_FreelancerId",
                table: "Proposals",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Jobs_JobId",
                table: "Proposals",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_FreelancerId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Jobs_JobId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proposals",
                table: "Proposals");

            migrationBuilder.RenameTable(
                name: "Proposals",
                newName: "Proposal");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_JobId",
                table: "Proposal",
                newName: "IX_Proposal_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_FreelancerId",
                table: "Proposal",
                newName: "IX_Proposal_FreelancerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proposal",
                table: "Proposal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_Jobs_JobId",
                table: "Proposal",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
