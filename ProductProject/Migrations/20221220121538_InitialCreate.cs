using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_User = table.Column<int>(type: "int", nullable: true),
                    Created_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_User = table.Column<int>(type: "int", nullable: true),
                    Modified_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category_Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_User = table.Column<int>(type: "int", nullable: true),
                    Created_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_User = table.Column<int>(type: "int", nullable: true),
                    Modified_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Attribute_Product_Category_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "Product_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Category_Id = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_User = table.Column<int>(type: "int", nullable: true),
                    Created_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_User = table.Column<int>(type: "int", nullable: true),
                    Modified_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Product_Category_Product_Category_Id",
                        column: x => x.Product_Category_Id,
                        principalTable: "Product_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryAttributeId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_User = table.Column<int>(type: "int", nullable: true),
                    Created_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_User = table.Column<int>(type: "int", nullable: true),
                    Modified_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Category_Attribute_CategoryAttributeId",
                        column: x => x.CategoryAttributeId,
                        principalTable: "Category_Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_User = table.Column<int>(type: "int", nullable: true),
                    Created_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_User = table.Column<int>(type: "int", nullable: true),
                    Modified_Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Attribute_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CategoryAttributeId",
                table: "Attribute",
                column: "CategoryAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Attribute_ProductCategoryId",
                table: "Category_Attribute",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Product_Category_Id",
                table: "Product",
                column: "Product_Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Attribute_ProductId",
                table: "Product_Attribute",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Product_Attribute");

            migrationBuilder.DropTable(
                name: "Category_Attribute");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Product_Category");
        }
    }
}
