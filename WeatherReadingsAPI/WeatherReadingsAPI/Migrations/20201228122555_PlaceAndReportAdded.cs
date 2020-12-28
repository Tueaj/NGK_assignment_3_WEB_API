using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherReadingsAPI.Migrations
{
    public partial class PlaceAndReportAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherRepport_Place_PlaceId",
                table: "WeatherRepport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherRepport",
                table: "WeatherRepport");

            migrationBuilder.RenameTable(
                name: "WeatherRepport",
                newName: "WReport");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherRepport_PlaceId",
                table: "WReport",
                newName: "IX_WReport_PlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WReport",
                table: "WReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WReport_Place_PlaceId",
                table: "WReport",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WReport_Place_PlaceId",
                table: "WReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WReport",
                table: "WReport");

            migrationBuilder.RenameTable(
                name: "WReport",
                newName: "WeatherRepport");

            migrationBuilder.RenameIndex(
                name: "IX_WReport_PlaceId",
                table: "WeatherRepport",
                newName: "IX_WeatherRepport_PlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherRepport",
                table: "WeatherRepport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRepport_Place_PlaceId",
                table: "WeatherRepport",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
