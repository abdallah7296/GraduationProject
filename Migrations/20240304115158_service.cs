using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    public partial class service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_playStations_PlayStationId",
                table: "games");

            migrationBuilder.AlterColumn<int>(
                name: "PlayStationId",
                table: "games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkOfPlace",
                table: "analysisCenters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_games_playStations_PlayStationId",
                table: "games",
                column: "PlayStationId",
                principalTable: "playStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_games_playStations_PlayStationId",
                table: "games");

            migrationBuilder.AlterColumn<int>(
                name: "PlayStationId",
                table: "games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LinkOfPlace",
                table: "analysisCenters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_games_playStations_PlayStationId",
                table: "games",
                column: "PlayStationId",
                principalTable: "playStations",
                principalColumn: "Id");
        }
    }
}
