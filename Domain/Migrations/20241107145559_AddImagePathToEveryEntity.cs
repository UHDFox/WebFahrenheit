using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToEveryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "WaterBoilers");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "WaterBoilers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Radiators");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Radiators");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Pumps");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Pumps");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Fireplaces");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Fireplaces");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "WaterBoilers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Radiators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Fireplaces",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "WaterBoilers");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Radiators");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Fireplaces");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "WaterBoilers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "WaterBoilers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Radiators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Radiators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Pumps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Pumps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Fireplaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Fireplaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
