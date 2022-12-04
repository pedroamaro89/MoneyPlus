using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPlus.Migrations
{
    /// <inheritdoc />
    public partial class alterNameColumnWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Wallet",
                newName: "Balance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Wallet",
                newName: "Amount");
        }
    }
}
