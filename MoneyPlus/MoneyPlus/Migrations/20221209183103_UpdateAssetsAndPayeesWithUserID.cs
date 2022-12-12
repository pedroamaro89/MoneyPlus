using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPlus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssetsAndPayeesWithUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Asset_AssetId",
                table: "Transaction");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NIF",
                table: "Payee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Asset",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Payee_UserId",
                table: "Payee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_UserId",
                table: "Asset",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_AspNetUsers_UserId",
                table: "Asset",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_Payee_AspNetUsers_UserId",
                table: "Payee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Asset_AssetId",
                table: "Transaction",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_AspNetUsers_UserId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Payee_AspNetUsers_UserId",
                table: "Payee");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Asset_AssetId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Payee_UserId",
                table: "Payee");

            migrationBuilder.DropIndex(
                name: "IX_Asset_UserId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payee");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Asset");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NIF",
                table: "Payee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Asset_AssetId",
                table: "Transaction",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
