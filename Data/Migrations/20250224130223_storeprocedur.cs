using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class storeprocedur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Apartment
            migrationBuilder.Sql(@"
            CREATE PROCEDURE InsertApartment
                @ApartmentName NVARCHAR(MAX),
                @Address NVARCHAR(MAX),
                @TotalUnits INT
            AS
            BEGIN
                SET NOCOUNT ON;
                INSERT INTO Apartments (ApartmentName, Address, TotalUnits)
                VALUES (@ApartmentName, @Address, @TotalUnits);
                SELECT SCOPE_IDENTITY() AS ApartmentID;
            END;
        ");

            // Get All Apartments
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetAllApartments
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT ApartmentID, ApartmentName, Address, TotalUnits FROM Apartments;
            END;
        ");

            // Get Apartment By ID
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetApartmentById
                @ApartmentID INT
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT ApartmentID, ApartmentName, Address, TotalUnits
                FROM Apartments WHERE ApartmentID = @ApartmentID;
            END;
        ");

            // Update Apartment
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateApartment
                @ApartmentID INT,
                @ApartmentName NVARCHAR(MAX),
                @Address NVARCHAR(MAX),
                @TotalUnits INT
            AS
            BEGIN
                SET NOCOUNT ON;
                UPDATE Apartments
                SET ApartmentName = @ApartmentName, Address = @Address, TotalUnits = @TotalUnits
                WHERE ApartmentID = @ApartmentID;
            END;
        ");

            // Delete Apartment
            migrationBuilder.Sql(@"
            CREATE PROCEDURE DeleteApartment
                @ApartmentID INT
            AS
            BEGIN
                SET NOCOUNT ON;
                DELETE FROM Apartments WHERE ApartmentID = @ApartmentID;
            END;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Stored Procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertApartment;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllApartments;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetApartmentById;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateApartment;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteApartment;");
        }
    }
}
