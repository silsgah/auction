using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Data.Migraions
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "color",
                table: "Item",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "winnerId",
                table: "Auctions",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Auctions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "soldAmount",
                table: "Auctions",
                newName: "SoldAmount");

            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Auctions",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Auctions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AutionEnd",
                table: "Auctions",
                newName: "AuctionEnd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Item",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Auctions",
                newName: "winnerId");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Auctions",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "SoldAmount",
                table: "Auctions",
                newName: "soldAmount");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Auctions",
                newName: "sellerId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Auctions",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "AuctionEnd",
                table: "Auctions",
                newName: "AutionEnd");
        }
    }
}
