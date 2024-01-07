using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicplayer.Migrations
{
    public partial class Migration100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Sounds");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Playlists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Sounds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
