using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelos.Migrations
{
    /// <inheritdoc />
    public partial class EditExitDateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExitTime",
                table: "Reservations",
                newName: "ExitDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExitDate",
                table: "Reservations",
                newName: "ExitTime");
        }
    }
}
