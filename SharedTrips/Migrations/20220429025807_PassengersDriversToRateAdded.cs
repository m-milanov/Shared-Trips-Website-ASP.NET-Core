using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedTrips.Migrations
{
    public partial class PassengersDriversToRateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassengerId",
                table: "Drivers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_PassengerId",
                table: "Drivers",
                column: "PassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_AspNetUsers_PassengerId",
                table: "Drivers",
                column: "PassengerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_AspNetUsers_PassengerId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_PassengerId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "Drivers");
        }
    }
}
