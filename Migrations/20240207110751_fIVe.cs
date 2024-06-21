using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    public partial class fIVe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_Doctors_DoctorId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_pharmacies_PharmaciesId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_playStations_PlayStationId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_Restaurants_RestaurantId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_workspaces_WorkspaceId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_DoctorId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_PharmaciesId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_PlayStationId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_RestaurantId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_WorkspaceId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "PharmaciesId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "PlayStationId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PharmaciesId",
                table: "images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayStationId",
                table: "images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkspaceId",
                table: "images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_DoctorId",
                table: "images",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_images_PharmaciesId",
                table: "images",
                column: "PharmaciesId");

            migrationBuilder.CreateIndex(
                name: "IX_images_PlayStationId",
                table: "images",
                column: "PlayStationId");

            migrationBuilder.CreateIndex(
                name: "IX_images_RestaurantId",
                table: "images",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_images_WorkspaceId",
                table: "images",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_images_Doctors_DoctorId",
                table: "images",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_pharmacies_PharmaciesId",
                table: "images",
                column: "PharmaciesId",
                principalTable: "pharmacies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_playStations_PlayStationId",
                table: "images",
                column: "PlayStationId",
                principalTable: "playStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_Restaurants_RestaurantId",
                table: "images",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_images_workspaces_WorkspaceId",
                table: "images",
                column: "WorkspaceId",
                principalTable: "workspaces",
                principalColumn: "Id");
        }
    }
}
