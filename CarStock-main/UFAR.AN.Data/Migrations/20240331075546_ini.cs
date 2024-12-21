using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UFAR.AN.Data.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOEM = table.Column<bool>(type: "bit", nullable: false),
                    OEM_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartVin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Items_ItemEntityId",
                        column: x => x.ItemEntityId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Description", "ItemEntityId", "Manufacturer", "Model", "Name", "StartVin", "Year" },
                values: new object[,]
                {
                    { 1, "Compact car produced by Toyota", null, "Toyota", "Corolla", "Toyota Corolla", "KMHDU8UDP", 2022 },
                    { 2, "Popular compact car manufactured by Honda", null, "Honda", "Civic", "Honda Civic", "JH4DA9350L", 2023 },
                    { 3, "Iconic American muscle car produced by Ford", null, "Ford", "Mustang", "Ford Mustang", "1G6KD54Y7V", 2024 },
                    { 4, "Luxury compact executive car manufactured by BMW", null, "BMW", "3 Series", "BMW 3 Series", "WAUZZZ8DZV", 2025 },
                    { 5, "Electric luxury sedan produced by Tesla, Inc.", null, "Tesla", "Model S", "Tesla Model S", "YV1LW5715P", 2026 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Condition", "Description", "IsOEM", "Location", "Name", "OEM_Number" },
                values: new object[,]
                {
                    { 1, "New", "Description1", true, "Location1", "Item1", "OEM1" },
                    { 2, "New", "Description2", true, "Location2", "Item2", "OEM2" },
                    { 3, "New", "Description3", true, "Location3", "Item3", "OEM3" },
                    { 4, "New", "Description4", true, "Location4", "Item4", "OEM4" },
                    { 5, "New", "Description5", true, "Location5", "Item5", "OEM5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ItemEntityId",
                table: "Cars",
                column: "ItemEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
