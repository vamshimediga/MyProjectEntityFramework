using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class personpassportsp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert stored procedure with output parameter
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertPerson]
                    @Name NVARCHAR(MAX),
                    @Active INT,
                    @Id INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    INSERT INTO [dbo].[Persons] (Name, Active)
                    VALUES (@Name, @Active);

                    -- Capture the inserted PersonId and return as output
                    SET @Id = SCOPE_IDENTITY();
                END
            ");

            // Update stored procedure with output parameter
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdatePerson]
                    @PersonId INT,
                    @Name NVARCHAR(MAX),
                    @Active INT,
                    @UpdatedPersonId INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    UPDATE [dbo].[Persons]
                    SET
                        Name = @Name,
                        Active = @Active
                    WHERE PersonId = @PersonId;

                    -- Set the updated PersonId as output
                    SET @UpdatedPersonId = @PersonId;
                END
            ");

            // Delete stored procedure with output parameter
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeletePerson]
                    @PersonId INT,
                    @DeletedPersonId INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DELETE FROM [dbo].[Persons]
                    WHERE PersonId = @PersonId;

                    -- Set the deleted PersonId as output
                    SET @DeletedPersonId = @PersonId;
                END
            ");

            // Get All Persons stored procedure (no changes here)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAllPersons]
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT PersonId, Name, Active
                    FROM [dbo].[Persons];
                END
            ");

            // Get Person by ID stored procedure (no changes here)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetPersonById]
                    @PersonId INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT PersonId, Name, Active
                    FROM [dbo].[Persons]
                    WHERE PersonId = @PersonId;
                END
            ");

            // Insert Passport stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertPassport]
                    @PassportNumber NVARCHAR(MAX),
                    @PersonId INT,
                    @portId INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    INSERT INTO [dbo].[Passports] (PassportNumber, PersonId)
                    VALUES (@PassportNumber, @PersonId);

                    -- Capture the inserted PassportId and return as output
                    SET @portId = SCOPE_IDENTITY();
                END
            ");

            // Update Passport stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdatePassport]
                    @PassportId INT,
                    @PassportNumber NVARCHAR(MAX),
                    @PersonId INT,
                    @UpdatedPassportId INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    UPDATE [dbo].[Passports]
                    SET
                        PassportNumber = @PassportNumber,
                        PersonId = @PersonId
                    WHERE PassportId = @PassportId;

                    -- Set the updated PassportId as output
                    SET @UpdatedPassportId = @PassportId;
                END
            ");

            // Delete Passport stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeletePassport]
                    @PassportId INT,
                    @DeletedPassportId INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DELETE FROM [dbo].[Passports]
                    WHERE PassportId = @PassportId;

                    -- Set the deleted PassportId as output
                    SET @DeletedPassportId = @PassportId;
                END
            ");

            // Get All Passports stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAllPassports]
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT PassportId, PassportNumber, PersonId
                    FROM [dbo].[Passports];
                END
            ");

            // Get Passport by Id stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetPassportById]
                    @PassportId INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT PassportId, PassportNumber, PersonId
                    FROM [dbo].[Passports]
                    WHERE PassportId = @PassportId;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop stored procedures in Down method
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertPerson]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdatePerson]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeletePerson]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllPersons]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetPersonById]");

            // Drop the stored procedures in Down method
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertPassport]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdatePassport]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeletePassport]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllPassports]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetPassportById]");
        }
    }
}
