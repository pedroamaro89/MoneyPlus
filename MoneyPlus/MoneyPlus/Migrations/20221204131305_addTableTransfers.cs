using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPlus.Migrations
{
    /// <inheritdoc />
    public partial class addTableTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Transfer",
               columns: table => new
               {
                   ID = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   OriginWalletID = table.Column<int>(type: "int", nullable: false),
                   DestinationWalletID = table.Column<int>(type: "int", nullable: false),
                   Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                   Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Amount = table.Column<double>(type: "float", nullable: false),

               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Transfer", x => x.ID);
                   table.ForeignKey(
                       name: "FK_Transfer_Wallet_OriginWalletID",
                       column: x => x.OriginWalletID,
                       principalTable: "Wallet",
                       principalColumn: "ID");
                   table.ForeignKey(
                     name: "FK_Transfer_Wallet_DestinationWalletID",
                     column: x => x.DestinationWalletID,
                     principalTable: "Wallet",
                     principalColumn: "ID");
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfer");
        }
    }
}
