using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_KeToan.Migrations
{
    /// <inheritdoc />
    public partial class addlieudd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedInterestPaid",
                table: "Loans");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedPrincipalPaid",
                table: "Payments",
                type: "decimal(15,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedPrincipalPaid",
                table: "Payments");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedInterestPaid",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
