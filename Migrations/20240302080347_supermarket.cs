using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    public partial class supermarket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_categories_CatId",
                table: "Doctors");

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
                name: "FK_workspaces_categories_CatId",
                table: "workspaces");

            migrationBuilder.DropTable(
                name: "categories");

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

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "workspaces");

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
                name: "CatId",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "analysisCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DescriptionOfPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkOfPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    StartWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    averageRate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysisCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DescriptionOfPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    StartWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    averageRate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperMarkets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analysisCenters");

            migrationBuilder.DropTable(
                name: "SuperMarkets");

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "workspaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

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
                name: "FK_workspaces_categories_CatId",
                table: "workspaces",
                column: "CatId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
