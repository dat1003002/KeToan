using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_KeToan.Migrations
{
    /// <inheritdoc />
    public partial class addtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrincipalPaymentAmount",
                table: "Loans",
                type: "decimal(15,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StartPrincipalPaymentMonth",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrincipalPaymentAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "StartPrincipalPaymentMonth",
                table: "Loans");
        }
    }
}
