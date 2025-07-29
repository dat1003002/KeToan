using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_KeToan.Migrations
{
    /// <inheritdoc />
    public partial class adddutinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedInterestPaid",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedInterestPaid",
                table: "Loans");
        }
    }
}
