using Microsoft.EntityFrameworkCore.Migrations;

namespace UserWebApp.Migrations
{
    public partial class AddLocalizationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Culture",
                table: "User",
                type: "nvarchar(max)",
                nullable: false, defaultValue: "en-US");

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                table: "User",
                type: "nvarchar(max)",
                nullable: false, defaultValue: "Jordan Standard Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Culture",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                table: "User");
        }
    }
}
