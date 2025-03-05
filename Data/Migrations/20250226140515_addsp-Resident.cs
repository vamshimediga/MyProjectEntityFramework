using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addspResident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[InsertResident]
                @Name NVARCHAR(MAX),
                @Age INT,
                @ApartmentID INT
            AS
            BEGIN
                SET NOCOUNT ON;
                INSERT INTO [dbo].[Residents] ([Name], [Age], [ApartmentID])
                VALUES (@Name, @Age, @ApartmentID);
                SELECT SCOPE_IDENTITY() AS ResidentID;
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[GetAllResidents]
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT ResidentID, Name, Age, ApartmentID FROM [dbo].[Residents];
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[GetResidentById]
                @ResidentID INT
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT ResidentID, Name, Age, ApartmentID 
                FROM [dbo].[Residents]
                WHERE ResidentID = @ResidentID;
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[UpdateResident]
                @ResidentID INT,
                @Name NVARCHAR(MAX),
                @Age INT,
                @ApartmentID INT
            AS
            BEGIN
                SET NOCOUNT ON;
                UPDATE [dbo].[Residents]
                SET Name = @Name, 
                    Age = @Age, 
                    ApartmentID = @ApartmentID
                WHERE ResidentID = @ResidentID;
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[DeleteResident]
                @ResidentID INT
            AS
            BEGIN
                SET NOCOUNT ON;
                DELETE FROM [dbo].[Residents]
                WHERE ResidentID = @ResidentID;
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertResident]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllResidents]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetResidentById]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateResident]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeleteResident]");
        }
    }
}
