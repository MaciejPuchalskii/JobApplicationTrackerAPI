using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoveJobAdvertURLToJobAppAddCompanyURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobAdvertURL",
                table: "Companies",
                newName: "CompanyURL");

            migrationBuilder.AddColumn<string>(
                name: "JobAdvertURL",
                table: "JobApplications",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobAdvertURL",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "CompanyURL",
                table: "Companies",
                newName: "JobAdvertURL");
        }
    }
}
