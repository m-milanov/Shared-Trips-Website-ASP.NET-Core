using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedTrips.Migrations
{
    public partial class TripPassengerAcceptedColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "TripPassenger",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "TripPassenger");
        }
    }
}
