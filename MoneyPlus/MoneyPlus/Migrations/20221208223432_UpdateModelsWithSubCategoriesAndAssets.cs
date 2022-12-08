using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPlus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsWithSubCategoriesAndAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Category_CategoryID",
                table: "Wallet");*/


            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transfer",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Transaction",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryID",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubID",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubName",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AssetId",
                table: "Transaction",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryID",
                table: "Transaction",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_SubCategoryID",
                table: "Transaction",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryId",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_CategoryId",
                table: "Category",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Asset_AssetId",
                table: "Transaction",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CategoryID",
                table: "Transaction",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_SubCategoryID",
                table: "Transaction",
                column: "SubCategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_CategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Asset_AssetId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CategoryID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_SubCategoryID",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AssetId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CategoryID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_SubCategoryID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SubCategoryID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SubID",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SubName",
                table: "Category");


            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transfer",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

        }
    }
}
