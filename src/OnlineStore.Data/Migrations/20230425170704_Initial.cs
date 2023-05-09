using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Options_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegularPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionValues",
                columns: table => new
                {
                    OptionValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionValues", x => x.OptionValueId);
                    table.ForeignKey(
                        name: "FK_OptionValues_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantOptionValues",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    OptionValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantOptionValues", x => new { x.VariantId, x.OptionValueId });
                    table.ForeignKey(
                        name: "FK_VariantOptionValues_OptionValues_OptionValueId",
                        column: x => x.OptionValueId,
                        principalTable: "OptionValues",
                        principalColumn: "OptionValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantOptionValues_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedDate", "Description", "IsVisible", "Label", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(2854), new TimeSpan(0, 0, 0, 0, 0)), "T-shirt the hate,100 cotton, machine embroidery, oversized fit.\r\n\r\nso much hate for me everywhere today, can't figure out what i did to cause it... i am trying to ovrercome, to overcome it all.", true, "preorder", "t-shirt the hate", null });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "CreatedDate", "Name", "ProductId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3052), new TimeSpan(0, 0, 0, 0, 0)), "Size", 1, null },
                    { 2, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3054), new TimeSpan(0, 0, 0, 0, 0)), "Color", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "VariantId", "CreatedDate", "ProductId", "Quantity", "RegularPrice", "SKU", "SalePrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3104), new TimeSpan(0, 0, 0, 0, 0)), 1, null, 3000m, "TTHS-MCB", null, null },
                    { 2, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3106), new TimeSpan(0, 0, 0, 0, 0)), 1, null, 3000m, "TTHS-MCW", null, null },
                    { 3, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3108), new TimeSpan(0, 0, 0, 0, 0)), 1, null, 3000m, "TTHM-LCB", null, null },
                    { 4, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3109), new TimeSpan(0, 0, 0, 0, 0)), 1, null, 3000m, "TTHM-LCW", null, null },
                    { 5, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3111), new TimeSpan(0, 0, 0, 0, 0)), 1, null, 3000m, "TTHXL+CB", null, null },
                    { 6, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3112), new TimeSpan(0, 0, 0, 0, 0)), 1, null, 3000m, "TTHXL+CW", null, null }
                });

            migrationBuilder.InsertData(
                table: "OptionValues",
                columns: new[] { "OptionValueId", "CreatedDate", "OptionId", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3074), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "s-m" },
                    { 2, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3076), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "m-l" },
                    { 3, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3077), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "xl+" },
                    { 4, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3078), new TimeSpan(0, 0, 0, 0, 0)), 2, null, "black" },
                    { 5, new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3080), new TimeSpan(0, 0, 0, 0, 0)), 2, null, "white" }
                });

            migrationBuilder.InsertData(
                table: "VariantOptionValues",
                columns: new[] { "OptionValueId", "VariantId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 5, 2 },
                    { 2, 3 },
                    { 4, 3 },
                    { 2, 4 },
                    { 5, 4 },
                    { 3, 5 },
                    { 4, 5 },
                    { 3, 6 },
                    { 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId",
                table: "Options",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_OptionId_Value",
                table: "OptionValues",
                columns: new[] { "OptionId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariantOptionValues_OptionValueId",
                table: "VariantOptionValues",
                column: "OptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantOptionValues");

            migrationBuilder.DropTable(
                name: "OptionValues");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
