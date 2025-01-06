using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Book_sp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create GetAllBooks stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAllBooks]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT BookId, Title, AuthorId
                    FROM [dbo].[Books];
                END;
            ");

            // Create GetBookById stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetBookById]
                    @BookId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT BookId, Title, AuthorId
                    FROM [dbo].[Books]
                    WHERE BookId = @BookId;
                END;
            ");

            // Create InsertBook stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertBook]
                    @Title NVARCHAR(MAX),
                    @AuthorId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO [dbo].[Books] (Title, AuthorId)
                    VALUES (@Title, @AuthorId);

                    SELECT SCOPE_IDENTITY() AS NewBookId;
                END;
            ");

            // Create UpdateBook stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdateBook]
                    @BookId INT,
                    @Title NVARCHAR(MAX),
                    @AuthorId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE [dbo].[Books]
                    SET 
                        Title = @Title,
                        AuthorId = @AuthorId
                    WHERE BookId = @BookId;

                    SELECT @@ROWCOUNT AS RowsAffected;
                END;
            ");

            // Create DeleteBook stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeleteBook]
                    @BookId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM [dbo].[Books]
                    WHERE BookId = @BookId;

                    SELECT @@ROWCOUNT AS RowsAffected;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop DeleteBook stored procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeleteBook]");

            // Drop UpdateBook stored procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateBook]");

            // Drop InsertBook stored procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertBook]");

            // Drop GetBookById stored procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetBookById]");

            // Drop GetAllBooks stored procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllBooks]");
        }
    }
}
