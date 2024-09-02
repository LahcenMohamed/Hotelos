using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelos.Migrations
{
    /// <inheritdoc />
    public partial class floorHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Floors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_HotelId",
                table: "Floors",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_Hotels_HotelId",
                table: "Floors",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_Hotels_HotelId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_HotelId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Floors");
        }
    }
}
