using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class companytypeoppertypetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    CompanyTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.CompanyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Opptypes",
                columns: table => new
                {
                    OpptypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpptypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opptypes", x => x.OpptypeID);
                    table.ForeignKey(
                        name: "FK_Opptypes_CompanyTypes_CompanyTypeID",
                        column: x => x.CompanyTypeID,
                        principalTable: "CompanyTypes",
                        principalColumn: "CompanyTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opptypes_CompanyTypeID",
                table: "Opptypes",
                column: "CompanyTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opptypes");

            migrationBuilder.DropTable(
                name: "CompanyTypes");
        }
    }
}
