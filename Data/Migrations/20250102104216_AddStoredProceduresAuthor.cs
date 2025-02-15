using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProceduresAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create GetAllAuthors stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAllAuthors]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT AuthorId, Name
                    FROM [dbo].[Authors];
                END;
            ");

            // Create GetAuthorById stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAuthorById]
                    @AuthorId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT AuthorId, Name
                    FROM [dbo].[Authors]
                    WHERE AuthorId = @AuthorId;
                END;
            ");

            // Create InsertAuthor stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertAuthor]
                    @Name NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO [dbo].[Authors] (Name)
                    VALUES (@Name);

                    SELECT SCOPE_IDENTITY() AS NewAuthorId;
                END;
            ");

            // Create UpdateAuthor stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdateAuthor]
                    @AuthorId INT,
                    @Name NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE [dbo].[Authors]
                    SET Name = @Name
                    WHERE AuthorId = @AuthorId;

                    SELECT @@ROWCOUNT AS RowsAffected;
                END;
            ");

            // Create DeleteAuthor stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeleteAuthor]
                    @AuthorId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM [dbo].[Authors]
                    WHERE AuthorId = @AuthorId;

                    SELECT @@ROWCOUNT AS RowsAffected;
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop GetAllAuthors stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[GetAllAuthors];");

            // Drop GetAuthorById stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[GetAuthorById];");

            // Drop InsertAuthor stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[InsertAuthor];");

            // Drop UpdateAuthor stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateAuthor];");

            // Drop DeleteAuthor stored procedure
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[DeleteAuthor];");
        }
    }
}
