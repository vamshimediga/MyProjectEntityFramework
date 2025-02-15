using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndPostAndSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertUser
                    @Username NVARCHAR(100),
                    @Email NVARCHAR(255),
                    @UserId INT OUTPUT
                AS
                BEGIN
                    INSERT INTO Users (Username, Email, CreatedAt)
                    VALUES (@Username, @Email, GETUTCDATE());

                    SET @UserId = SCOPE_IDENTITY();
                END
            ");

            // Create UpdateUser stored procedure with output parameter
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateUser
                    @UserId INT,
                    @Username NVARCHAR(100),
                    @Email NVARCHAR(255),
                    @Result INT OUTPUT
                AS
                BEGIN
                    UPDATE Users
                    SET Username = @Username, Email = @Email
                    WHERE UserId = @UserId;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1; -- Success
                    ELSE
                        SET @Result = 0; -- No rows updated
                END
            ");

            // Create DeleteUser stored procedure with output parameter
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteUser
                    @UserId INT,
                    @Result INT OUTPUT
                AS
                BEGIN
                    DELETE FROM Users WHERE UserId = @UserId;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1; -- Success
                    ELSE
                        SET @Result = 0; -- No rows deleted
                END
            ");

            // Create GetUsers stored procedure (returns all users)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetUsers
                AS
                BEGIN
                    SELECT UserId, Username, Email, CreatedAt
                    FROM Users;
                END
            ");

            // Create GetUserById stored procedure (returns a user by UserId)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetUserById
                    @UserId INT
                AS
                BEGIN
                    SELECT UserId, Username, Email, CreatedAt
                    FROM Users
                    WHERE UserId = @UserId;
                END
            ");
            // Add InsertPost stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertPost
                    @UserId INT,
                    @Title NVARCHAR(MAX),
                    @Content NVARCHAR(MAX),
                    @PublishedAt DATETIME2,
                    @PostId INT OUTPUT
                AS
                BEGIN
                    INSERT INTO [dbo].[Posts] (UserId, Title, Content, PublishedAt)
                    VALUES (@UserId, @Title, @Content, @PublishedAt);
                    
                    SET @PostId = SCOPE_IDENTITY();
                END;
            ");

            // Add UpdatePost stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdatePost
                    @PostId INT,
                    @UserId INT,
                    @Title NVARCHAR(MAX),
                    @Content NVARCHAR(MAX),
                    @PublishedAt DATETIME2,
                    @Result INT OUTPUT
                AS
                BEGIN
                    UPDATE [dbo].[Posts]
                    SET Title = @Title, Content = @Content, PublishedAt = @PublishedAt
                    WHERE PostId = @PostId AND UserId = @UserId;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1;
                    ELSE
                        SET @Result = 0;
                END;
            ");

            // Add DeletePost stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeletePost
                    @PostId INT,
                    @Result INT OUTPUT
                AS
                BEGIN
                    DELETE FROM [dbo].[Posts]
                    WHERE PostId = @PostId;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1;
                    ELSE
                        SET @Result = 0;
                END;
            ");

            // Add GetPosts stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetPosts
                AS
                BEGIN
                    SELECT PostId, UserId, Title, Content, PublishedAt
                    FROM [dbo].[Posts];
                END;
            ");

            // Add GetPostById stored procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetPostById
                    @PostId INT
                AS
                BEGIN
                    SELECT PostId, UserId, Title, Content, PublishedAt
                    FROM [dbo].[Posts]
                    WHERE PostId = @PostId;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop stored procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertUser");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateUser");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteUser");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetUsers");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetUserById");

            // Drop Users table
            migrationBuilder.DropTable(name: "Users");

            // Drop the stored procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertPost;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdatePost;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeletePost;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetPosts;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetPostById;");

            // Drop the Posts table
            migrationBuilder.DropTable(
                name: "Posts");
        }


    }


}
