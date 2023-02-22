using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSpot_v3.Infrastructure.Migrations
{
    public partial class addAlbumUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Albums");
        }
    }
}
