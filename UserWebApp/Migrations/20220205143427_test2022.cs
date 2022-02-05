using Microsoft.EntityFrameworkCore.Migrations;

namespace UserWebApp.Migrations
{
    public partial class test2022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Restaurants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsWithinDistance",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsWithinDistance",
                table: "Restaurants");
        }
    }
}
