using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class PicUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fireplaces");

            migrationBuilder.DropTable(
                name: "Pumps");

            migrationBuilder.DropTable(
                name: "Radiators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WaterBoilers",
                table: "WaterBoilers");

            migrationBuilder.RenameTable(
                name: "WaterBoilers",
                newName: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "MaxTemperature",
                table: "Product",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<double>(
                name: "HeatedValue",
                table: "Product",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Product",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FireLevel",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuelUsage",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PowerSupply",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pressure",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WaterBoiler_HeatedValue",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterBoiler_Material",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FireLevel",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FuelUsage",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PowerSupply",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WaterBoiler_HeatedValue",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WaterBoiler_Material",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "WaterBoilers");

            migrationBuilder.AlterColumn<int>(
                name: "MaxTemperature",
                table: "WaterBoilers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "WaterBoilers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HeatedValue",
                table: "WaterBoilers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WaterBoilers",
                table: "WaterBoilers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Fireplaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FireLevel = table.Column<int>(type: "integer", nullable: false),
                    FuelUsage = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fireplaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pumps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PowerSupply = table.Column<int>(type: "integer", nullable: false),
                    Pressure = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pumps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Radiators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeatedValue = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Material = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radiators", x => x.Id);
                });
        }
    }
}
