using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CoalmineCoalPeoduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoalMines",
                columns: table => new
                {
                    MineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacityInTons = table.Column<int>(type: "int", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoalMines", x => x.MineID);
                });

            migrationBuilder.CreateTable(
                name: "CoalProductions",
                columns: table => new
                {
                    ProductionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionYear = table.Column<int>(type: "int", nullable: false),
                    TotalProductionInTons = table.Column<int>(type: "int", nullable: false),
                    MineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoalProductions", x => x.ProductionID);
                    table.ForeignKey(
                        name: "FK_CoalProductions_CoalMines_MineID",
                        column: x => x.MineID,
                        principalTable: "CoalMines",
                        principalColumn: "MineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoalProductions_MineID",
                table: "CoalProductions",
                column: "MineID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoalProductions");

            migrationBuilder.DropTable(
                name: "CoalMines");
        }
    }
}
