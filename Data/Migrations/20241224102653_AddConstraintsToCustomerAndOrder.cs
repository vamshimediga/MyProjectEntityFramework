using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintsToCustomerAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_TotalAmount_Length",
                table: "Order",
                sql: "TotalAmount < 1000");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email_Unique",
                table: "Customer",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_TotalAmount_Length",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Email_Unique",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
