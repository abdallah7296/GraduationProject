using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Restaurants_RestId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_images_Doctors_DoctorID",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_pharmacies_PharmaciesID",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_playStations_PlaystationID",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_Restaurants_RestID",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_workspaces_workspaceID",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_pharmacies_pharmacieId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_playStations_PlaystationId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RestId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RestId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameColumn(
                name: "PlaystationId",
                table: "Reviews",
                newName: "PlayStationId");

            migrationBuilder.RenameColumn(
                name: "pharmacieId",
                table: "Reviews",
                newName: "RestaurantId");

            migrationBuilder.RenameColumn(
                name: "RestId",
                table: "Reviews",
                newName: "PharmaciesId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PlaystationId",
                table: "Reviews",
                newName: "IX_Reviews_PlayStationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RestId",
                table: "Reviews",
                newName: "IX_Reviews_PharmaciesId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_pharmacieId",
                table: "Reviews",
                newName: "IX_Reviews_RestaurantId");

            migrationBuilder.RenameColumn(
                name: "workspaceID",
                table: "images",
                newName: "WorkspaceId");

            migrationBuilder.RenameColumn(
                name: "PlaystationID",
                table: "images",
                newName: "PlayStationId");

            migrationBuilder.RenameColumn(
                name: "PharmaciesID",
                table: "images",
                newName: "PharmaciesId");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "images",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "RestID",
                table: "images",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_images_workspaceID",
                table: "images",
                newName: "IX_images_WorkspaceId");

            migrationBuilder.RenameIndex(
                name: "IX_images_PlaystationID",
                table: "images",
                newName: "IX_images_PlayStationId");

            migrationBuilder.RenameIndex(
                name: "IX_images_PharmaciesID",
                table: "images",
                newName: "IX_images_PharmaciesId");

            migrationBuilder.RenameIndex(
                name: "IX_images_DoctorID",
                table: "images",
                newName: "IX_images_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_images_RestID",
                table: "images",
                newName: "IX_images_RestaurantId");

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "workspaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "serviceName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "playStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "pharmacies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfMeal",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicId",
                table: "images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "serviceName",
                table: "images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_workspaces_CatId",
                table: "workspaces",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CatId",
                table: "Restaurants",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_playStations_CatId",
                table: "playStations",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacies_CatId",
                table: "pharmacies",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CatId",
                table: "Doctors",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_categories_CatId",
                table: "Doctors",
                column: "CatId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_pharmacies_categories_CatId",
                table: "pharmacies",
                column: "CatId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_playStations_categories_CatId",
                table: "playStations",
                column: "CatId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_categories_CatId",
                table: "Restaurants",
                column: "CatId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_workspaces_categories_CatId",
                table: "workspaces",
                column: "CatId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_categories_CatId",
                table: "Doctors");

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

            migrationBuilder.DropForeignKey(
                name: "FK_pharmacies_categories_CatId",
                table: "pharmacies");

            migrationBuilder.DropForeignKey(
                name: "FK_playStations_categories_CatId",
                table: "playStations");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_categories_CatId",
                table: "Restaurants");

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
                name: "FK_workspaces_categories_CatId",
                table: "workspaces");

            migrationBuilder.DropIndex(
                name: "IX_workspaces_CatId",
                table: "workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CatId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_playStations_CatId",
                table: "playStations");

            migrationBuilder.DropIndex(
                name: "IX_pharmacies_CatId",
                table: "pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_CatId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "ServicId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "serviceName",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "playStations");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "pharmacies");

            migrationBuilder.DropColumn(
                name: "TypeOfMeal",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "ServicId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "serviceName",
                table: "images");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "PlayStationId",
                table: "Reviews",
                newName: "PlaystationId");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Reviews",
                newName: "pharmacieId");

            migrationBuilder.RenameColumn(
                name: "PharmaciesId",
                table: "Reviews",
                newName: "RestId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PlayStationId",
                table: "Reviews",
                newName: "IX_Reviews_PlaystationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews",
                newName: "IX_Reviews_pharmacieId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PharmaciesId",
                table: "Reviews",
                newName: "IX_Reviews_RestId");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "images",
                newName: "workspaceID");

            migrationBuilder.RenameColumn(
                name: "PlayStationId",
                table: "images",
                newName: "PlaystationID");

            migrationBuilder.RenameColumn(
                name: "PharmaciesId",
                table: "images",
                newName: "PharmaciesID");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "images",
                newName: "DoctorID");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "images",
                newName: "RestID");

            migrationBuilder.RenameIndex(
                name: "IX_images_WorkspaceId",
                table: "images",
                newName: "IX_images_workspaceID");

            migrationBuilder.RenameIndex(
                name: "IX_images_PlayStationId",
                table: "images",
                newName: "IX_images_PlaystationID");

            migrationBuilder.RenameIndex(
                name: "IX_images_PharmaciesId",
                table: "images",
                newName: "IX_images_PharmaciesID");

            migrationBuilder.RenameIndex(
                name: "IX_images_DoctorId",
                table: "images",
                newName: "IX_images_DoctorID");

            migrationBuilder.RenameIndex(
                name: "IX_images_RestaurantId",
                table: "images",
                newName: "IX_images_RestID");

            migrationBuilder.AddColumn<int>(
                name: "RestId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RestId",
                table: "Categories",
                column: "RestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Restaurants_RestId",
                table: "Categories",
                column: "RestId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_images_Doctors_DoctorID",
                table: "images",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_pharmacies_PharmaciesID",
                table: "images",
                column: "PharmaciesID",
                principalTable: "pharmacies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_playStations_PlaystationID",
                table: "images",
                column: "PlaystationID",
                principalTable: "playStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_Restaurants_RestID",
                table: "images",
                column: "RestID",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_images_workspaces_workspaceID",
                table: "images",
                column: "workspaceID",
                principalTable: "workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_pharmacies_pharmacieId",
                table: "Reviews",
                column: "pharmacieId",
                principalTable: "pharmacies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_playStations_PlaystationId",
                table: "Reviews",
                column: "PlaystationId",
                principalTable: "playStations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestId",
                table: "Reviews",
                column: "RestId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId");
        }
    }
}
