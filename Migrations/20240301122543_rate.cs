using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    public partial class rate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_pharmacies_PharmaciesId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_playStations_PlayStationId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_workspaces_WorkspaceId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_DoctorId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PharmaciesId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PlayStationId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_WorkspaceId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PharmaciesId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PlayStationId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "averageRate",
                table: "workspaces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "averageRate",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "averageRate",
                table: "playStations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "averageRate",
                table: "pharmacies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "averageRate",
                table: "Doctors",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "averageRate",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "averageRate",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "averageRate",
                table: "playStations");

            migrationBuilder.DropColumn(
                name: "averageRate",
                table: "pharmacies");

            migrationBuilder.DropColumn(
                name: "averageRate",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PharmaciesId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayStationId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkspaceId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DoctorId",
                table: "Reviews",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PharmaciesId",
                table: "Reviews",
                column: "PharmaciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlayStationId",
                table: "Reviews",
                column: "PlayStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WorkspaceId",
                table: "Reviews",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_pharmacies_PharmaciesId",
                table: "Reviews",
                column: "PharmaciesId",
                principalTable: "pharmacies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_playStations_PlayStationId",
                table: "Reviews",
                column: "PlayStationId",
                principalTable: "playStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_workspaces_WorkspaceId",
                table: "Reviews",
                column: "WorkspaceId",
                principalTable: "workspaces",
                principalColumn: "Id");
        }
    }
}
