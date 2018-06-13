using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgency.Storage.Migrations
{
    public partial class Reservation_CommentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reservations");
        }
    }
}
