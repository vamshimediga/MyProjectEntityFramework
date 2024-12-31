using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProceduresInstitute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create GetAllInstitutes stored procedure
            migrationBuilder.Sql(@"
        CREATE PROCEDURE [dbo].[GetAllInstitutes]
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT InstituteId, InstituteName
            FROM [dbo].[Institutes];
        END;
    ");

            // Create GetInstituteById stored procedure
            migrationBuilder.Sql(@"
        CREATE PROCEDURE [dbo].[GetInstituteById]
            @InstituteId INT
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT InstituteId, InstituteName
            FROM [dbo].[Institutes]
            WHERE InstituteId = @InstituteId;
        END;
    ");

            // Create InsertInstitute stored procedure
            migrationBuilder.Sql(@"
        CREATE PROCEDURE [dbo].[InsertInstitute]
            @InstituteName NVARCHAR(MAX)
        AS
        BEGIN
            SET NOCOUNT ON;
            INSERT INTO [dbo].[Institutes] (InstituteName)
            VALUES (@InstituteName);

            SELECT SCOPE_IDENTITY() AS NewInstituteId;
        END;
    ");

            // Create UpdateInstitute stored procedure
            migrationBuilder.Sql(@"
        CREATE PROCEDURE [dbo].[UpdateInstitute]
            @InstituteId INT,
            @InstituteName NVARCHAR(MAX)
        AS
        BEGIN
            SET NOCOUNT ON;
            UPDATE [dbo].[Institutes]
            SET InstituteName = @InstituteName
            WHERE InstituteId = @InstituteId;

            SELECT @@ROWCOUNT AS RowsAffected;
        END;
    ");

            // Create DeleteInstitute stored procedure
            migrationBuilder.Sql(@"
        CREATE PROCEDURE [dbo].[DeleteInstitute]
            @InstituteId INT
        AS
        BEGIN
            SET NOCOUNT ON;
            DELETE FROM [dbo].[Institutes]
            WHERE InstituteId = @InstituteId;

            SELECT @@ROWCOUNT AS RowsAffected;
        END;
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop GetAllInstitutes stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[GetAllInstitutes];");

            // Drop GetInstituteById stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[GetInstituteById];");

            // Drop InsertInstitute stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[InsertInstitute];");

            // Drop UpdateInstitute stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateInstitute];");

            // Drop DeleteInstitute stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[DeleteInstitute];");
        }

    }
}
