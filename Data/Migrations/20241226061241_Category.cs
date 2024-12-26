using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Insert Category
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertCategory]
                    @CategoryName NVARCHAR(MAX),
                    @Description NVARCHAR(MAX)
                AS
                BEGIN
                    INSERT INTO Categories (CategoryName, Description)
                    VALUES (@CategoryName, @Description)
                END
            ");

            // Stored Procedure: Update Category
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdateCategory]
                    @CategoryId INT,
                    @CategoryName NVARCHAR(MAX),
                    @Description NVARCHAR(MAX)
                AS
                BEGIN
                    UPDATE Categories
                    SET CategoryName = @CategoryName,
                        Description = @Description
                    WHERE CategoryId = @CategoryId
                END
            ");

            // Stored Procedure: Delete Category
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeleteCategory]
                    @CategoryId INT
                AS
                BEGIN
                    DELETE FROM Categories
                    WHERE CategoryId = @CategoryId
                END
            ");

            // Stored Procedure: Get All Categories
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAllCategories]
                AS
                BEGIN
                    SELECT CategoryId, CategoryName, Description
                    FROM Categories
                END
            ");

            // Stored Procedure: Get Category By ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetCategoryById]
                    @CategoryId INT
                AS
                BEGIN
                    SELECT CategoryId, CategoryName, Description
                    FROM Categories
                    WHERE CategoryId = @CategoryId
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Stored Procedure: Insert Category
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertCategory]");

            // Drop Stored Procedure: Update Category
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateCategory]");

            // Drop Stored Procedure: Delete Category
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeleteCategory]");

            // Drop Stored Procedure: Get All Categories
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllCategories]");

            // Drop Stored Procedure: Get Category By ID
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetCategoryById]");
        }
    }
}
